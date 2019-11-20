using BrimstoneMissionGenerator.Models;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Tortuga.Anchor;

namespace BrimstoneMissionGenerator.Services
{
    public class MissionService
    {
        public ReadOnlyCollection<MissionSet> Sets;

        public MissionService(IWebHostEnvironment environment)
        {
            var file = new FileInfo(Path.Combine(environment.WebRootPath, @"App_Data/Missions.xml"));
            var settingXmlSerializer = new XmlSerializer(typeof(Models.Xml.Missions));

            Models.Xml.Missions sets;
            using (var stream = file.OpenRead())
                sets = (Models.Xml.Missions)settingXmlSerializer.Deserialize(stream);

            var products = XElement.Load(Path.Combine(environment.WebRootPath, @"App_Data/Products.xml"));

            var realSets = new List<MissionSet>();
            foreach (var item in sets.Set)
            {
                var product = products.Elements().SingleOrDefault(x => x.Attribute("Id").Value == item.Id.ToString());

                var productHtml = product != null ? new MarkupString(product.Value) : (MarkupString?)null;

                realSets.Add(new MissionSet(item.Id,
                                 item.Name,
                                 item.Mission.Select(x => new Mission(x.Name,
                                                              x.Number,
                                                              x.Page,
                                                              x.Location,
                                                              x.RandomWorlds,
                                                              x.Notes,
                                                              x.Intro,
                                                              x.EnemyTheme?.Select(y => y.Name) ?? Enumerable.Empty<string>(),
                                                              x.Rule?.Select(y => y.Name) ?? Enumerable.Empty<string>(),
                                                              x.Objective?.Select(y => y.Name) ?? Enumerable.Empty<string>(),
                                                              x.Token?.Select(y => y.Name) ?? Enumerable.Empty<string>()
                                                              )).ToList(),
                                 item.BggUrl,
                                 item.DownloadUrl,
                                 item.OtherWorld,
                                 productHtml));
            }

            Sets = new ReadOnlyCollection<MissionSet>(realSets);
        }

        public async Task<MissionPicker> GenerateMissionAsync(LocalStorage? localStorage, int setting, int mission)
        {
            var set = Sets.Single(x => x.Id == setting);
            var selectedMission = new MissionPicker(set, set.Missions.Single(x => x.Number == mission));

            if (localStorage != null)
                Fixup(new RandomExtended(), (await FindSetsAsync(localStorage)).Where(x => x.Available).ToList(), selectedMission);

            return selectedMission;
        }

        public async Task<MissionPicker> GenerateMissionAsync(LocalStorage? localStorage)
        {
            var sets = await FindSetsAsync(localStorage);

            if (localStorage == null) //pre-rendering, include all missions
                foreach (var set in sets)
                    set.Available = true;

            var missions = new List<MissionPicker>();
            foreach (var set in sets.Where(x => x.Available))
                foreach (var mission in set.MissionsSet.Missions)
                    missions.Add(new MissionPicker(set.MissionsSet, mission));

            var dice = new RandomExtended();
            var selectedMission = dice.Choose(missions);

            Fixup(dice, sets, selectedMission);

            return selectedMission;
        }

        void Fixup(RandomExtended dice, List<CheckList> sets, MissionPicker missionPicker)
        {
            if (missionPicker.Mission.RandomWorlds > 0)
            {
                var myWorlds = sets.Where(x => !string.IsNullOrEmpty(x.MissionsSet.OtherWorld)).Select(x => x.MissionsSet.OtherWorld.Split(',')).SelectMany(x => x).Select(x => x.Trim()).Distinct().ToList();

                var missionWorlds = missionPicker.Mission.Locations.ToList();
                myWorlds = myWorlds.Where(x => !missionWorlds.Contains(x)).ToList();

                if (myWorlds.Count > missionPicker.Mission.RandomWorlds)
                    missionPicker.OtherWorlds.AddRange(dice.Choose(myWorlds, missionPicker.Mission.RandomWorlds.Value, false)); //no duplicates
                else if (myWorlds.Count > 0)
                    missionPicker.OtherWorlds.AddRange(dice.Choose(myWorlds, missionPicker.Mission.RandomWorlds.Value)); //duplicates needed
            }
        }

        public async Task SaveSetsAsync(LocalStorage localStorage, List<CheckList>? sets)
        {
            var selectedSets = string.Join(",", sets.Where(x => x.Available).Select(x => x.MissionsSet.Id.ToString()));

            await localStorage.SetItemAsync("missions", selectedSets);
        }

        public async Task<List<CheckList>> FindSetsAsync(LocalStorage? localStorage)
        {
            var sets = Sets.Select(x => new CheckList(x)).ToList();

            if (localStorage != null)
            {
                var listRaw = await localStorage.GetItemAsync("missions");
                if (listRaw != null)
                {
                    var list = listRaw.Split(',').Select(x => int.Parse(x)).ToList();

                    foreach (var set in sets)
                    {
                        if (list.Contains(set.MissionsSet.Id))
                            set.Available = true;
                    }
                }
            }

            if (!sets.Any(x => x.Available))
                sets[0].Available = true;
            return sets;
        }
    }
}
