using BrimstoneMissionGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tortuga.Anchor;

namespace BrimstoneMissionGenerator.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private const string MissionsCookieName = "missions";

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult AllMissions()
        {
            return View(Application.Sets);
        }

        public ActionResult Index()
        {
            var sets = FindSets();

            return View(sets);
        }

        [HttpPost]
        public ActionResult Mission(FormCollection formData)
        {
            var selectedMissions = formData.AllKeys.Where(x => formData[x].Contains("true")).Select(x => x.Substring("Mission_".Length)).ToList();
            var userCookie = new HttpCookie(MissionsCookieName, string.Join(",", selectedMissions));
            userCookie.Expires = DateTime.Now.AddYears(1);
            Response.SetCookie(userCookie);

            return Redirect("/Home/Mission");
        }

        [HttpGet]
        public ActionResult Mission()
        {
            var sets = FindSets().Where(x => x.Available == true).ToList();
            var missions = new List<MissionPicker>();
            foreach (var set in sets)
                foreach (var mission in set.MissionsSet.Missions)
                    missions.Add(new MissionPicker() { MissionsSet = set.MissionsSet, Mission = mission });

            var dice = new RandomExtended();
            var selectedMission = dice.Choose(missions);

            Fixup(dice, sets, selectedMission);

            return View(selectedMission);
        }

        [HttpGet]
        [Route("Mission/{setting}/{mission}")]
        public ActionResult Mission(int setting, int mission)
        {
            var set = Application.Sets.Single(x => x.Id == setting);
            var selectedMission = new MissionPicker() { MissionsSet = set, Mission = set.Missions[mission] };

            Fixup(new RandomExtended(), FindSets().Where(x => x.Available == true).ToList(), selectedMission);

            return View(selectedMission);
        }

        private List<CheckList> FindSets()
        {
            var sets = Application.Sets.Select(x => new CheckList() { MissionsSet = x }).ToList();

            if (Request.Cookies[MissionsCookieName] != null && !string.IsNullOrEmpty(Request.Cookies[MissionsCookieName].Value))
            {
                var list = Request.Cookies[MissionsCookieName].Value.Split(',').Select(x => int.Parse(x)).ToList();
                foreach (var set in sets)
                {
                    if (list.Contains(set.MissionsSet.Id))
                        set.Available = true;
                }
            }

            if (!sets.Any(x => x.Available))
                sets[0].Available = true;
            return sets;
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
    }
}