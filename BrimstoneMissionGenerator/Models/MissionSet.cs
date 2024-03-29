﻿using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BrimstoneMissionGenerator.Models
{
    public class MissionSet
    {
        public MissionSet(int id, string name, IList<Mission> missions, string bggUrl, string downloadUrl, string otherWorld/*, MarkupString? productHtml*/)
        {
            Id = id;
            Name = name;
            Missions = new ReadOnlyCollection<Mission>(missions);
            BggUrl = bggUrl;
            DownloadUrl = downloadUrl;
            OtherWorld = otherWorld;
            //ProductHtml = productHtml;

            if (name.StartsWith("The ", System.StringComparison.OrdinalIgnoreCase))
                NameForSorting = name.Substring(4) + ", The";
            else if (name.StartsWith("A ", System.StringComparison.OrdinalIgnoreCase))
                NameForSorting = name.Substring(4) + ", A";
            else
                NameForSorting = name;
        }

        public string BggUrl { get; }
        public string DownloadUrl { get; }
        public int Id { get; }

        public string Locations
        {
            get
            {
                var values = Missions.SelectMany(x => x.Locations).Distinct();
                return string.Join(", ", values);
            }
        }

        public MarkupString MissionNamesHtml
        {
            get
            {
                var values = Missions.Select(x => (x.Number != 0 ? x.Number + ". " : "") + x.Name);
                //return new MarkupString(string.Join("<br>", values));
                return new MarkupString(string.Join("\r\n", values));
            }
        }

        public ReadOnlyCollection<Mission> Missions { get; }
        public string Name { get; }
        public string NameForSorting { get; }
        public string OtherWorld { get; }
        //public MarkupString? ProductHtml { get; }
    }
}
