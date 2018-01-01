using BrimstoneMissionGenerator.Models.Xml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;

namespace BrimstoneMissionGenerator.Models
{
    public class Product
    {

        public Product(int id, string name, IList<Mission> missions, string bggUrl, string downloadUrl, string otherWorld, MvcHtmlString productHtml)
        {
            Id = id;
            Name = name;
            Missions = new ReadOnlyCollection<Mission>(missions);
            BggUrl = bggUrl;
            DownloadUrl = downloadUrl;
            OtherWorld = otherWorld;
            ProductHtml = productHtml;
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

        public MvcHtmlString MissionNamesHtml
        {
            get
            {
                var values = Missions.Select(x => (x.Number.HasValue ? x.Number + ". " : "") + x.Name);
                return new MvcHtmlString(string.Join("&#013;", values));
            }
        }

        public ReadOnlyCollection<Mission> Missions { get; }
        public string Name { get; }
        public string OtherWorld { get; }
        public MvcHtmlString ProductHtml { get; }
    }
}