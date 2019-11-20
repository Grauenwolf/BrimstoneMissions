namespace BrimstoneMissionGenerator.Models
{
    public class CheckList
    {
        public CheckList(MissionSet missionSet)
        {
            MissionsSet = missionSet;
        }

        public MissionSet MissionsSet { get; }
        public bool Available { get; set; }
    }
}
