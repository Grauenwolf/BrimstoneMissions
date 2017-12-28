namespace BrimstoneMissionGenerator.Models
{
    public class CheckList
    {
        public MissionsSet MissionsSet { get; set; }
        public bool Available { get; set; }
    }

    public class MissionPicker
    {
        public MissionsSet MissionsSet { get; set; }
        public MissionsSetMission Mission { get; set; }
    }
}
