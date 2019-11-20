using System;
using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable

namespace BrimstoneMissionGenerator.Models.Xml
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Missions
    {
        [XmlElement("Set")]
        public MissionsSet[] Set { get; set; }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class MissionsSet
    {
        [XmlAttribute()]
        public string BggUrl { get; set; }

        [XmlAttribute()]
        public string DownloadUrl { get; set; }

        [XmlAttribute()]
        public int Id { get; set; }

        [XmlElement("Mission")]
        public MissionsSetMission[] Mission { get; set; }

        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public string OtherWorld { get; set; }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class MissionsSetMission
    {
        [XmlElement("EnemyTheme")]
        public MissionsSetMissionEnemyTheme[] EnemyTheme { get; set; }

        [XmlAttribute()]
        public string Intro { get; set; }

        [XmlAttribute()]
        public string Location { get; set; }

        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public string Notes { get; set; }

        [XmlAttribute()]
        public int Number { get; set; }

        [XmlIgnore()]
        public bool NumberSpecified { get; set; }

        [XmlElement("Objective")]
        public MissionsSetMissionObjective[] Objective { get; set; }

        [XmlAttribute()]
        public int Page { get; set; }

        [XmlIgnore()]
        public bool PageSpecified { get; set; }

        [XmlAttribute()]
        public int RandomWorlds { get; set; }

        [XmlIgnore()]
        public bool RandomWorldsSpecified { get; set; }

        [XmlElement("Rule")]
        public MissionsSetMissionRule[] Rule { get; set; }

        [XmlElement("Token")]
        public MissionsSetMissionToken[] Token { get; set; }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class MissionsSetMissionEnemyTheme
    {
        [XmlAttribute()]
        public string Name { get; set; }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class MissionsSetMissionObjective
    {
        [XmlAttribute()]
        public string Name { get; set; }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class MissionsSetMissionRule
    {
        [XmlAttribute()]
        public string Name { get; set; }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class MissionsSetMissionToken
    {
        [XmlAttribute()]
        public string Name { get; set; }
    }
}
