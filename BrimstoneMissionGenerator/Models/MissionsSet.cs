using System.Linq;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace BrimstoneMissionGenerator.Models
{
    partial class MissionsSet
    {
        [XmlIgnore]
        public MvcHtmlString ProductHtml { get; set; }

        [XmlIgnore]
        public string Locations
        {
            get
            {
                var values = Mission.Where(x => !string.IsNullOrWhiteSpace(x.Location)).Select(x => x.Location.Split(',')).SelectMany(x => x).Select(x => x.Trim()).Distinct();
                return string.Join(", ", values);
            }
        }

        public MvcHtmlString MissionNameHtml
        {
            get
            {
                var values = Mission.Select(x => (x.NumberSpecified ? x.Number + ". " : "") + x.Name);
                return new MvcHtmlString(string.Join("&#013;", values));
            }
        }
    }
}