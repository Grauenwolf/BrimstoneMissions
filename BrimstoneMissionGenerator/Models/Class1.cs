﻿using System;
using System.ComponentModel;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace BrimstoneMissionGenerator.Models
{



    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Missions
    {

        /// <remarks/>
        [XmlElement("Set")]
        public MissionsSet[] Set { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class MissionsSet
    {

        /// <remarks/>
        [XmlElement("Mission")]
        public MissionsSetMission[] Mission { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string DownloadUrl { get; set; }
        [XmlAttribute()]
        public int Id { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string BggUrl { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class MissionsSetMission
    {

        /// <remarks/>
        [XmlAttribute()]
        public byte Number { get; set; }

        /// <remarks/>
        [XmlIgnore()]
        public bool NumberSpecified { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Location { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Notes { get; set; }
    }


}