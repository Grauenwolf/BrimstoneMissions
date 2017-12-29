using System;
using System.ComponentModel;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.Web.Mvc;

namespace BrimstoneMissionGenerator.Models
{
    partial class MissionsSet
    {
        [XmlIgnore]
        public MvcHtmlString ProductHtml { get; set; }
    }
}