using System.Collections.Generic;

namespace BrimstoneMissionGenerator.Models
{
    public class MissionPicker
    {
        public MissionPicker(MissionSet missionsSet, Mission mission)
        {
            MissionsSet = missionsSet;
            Mission = mission;
        }

        public MissionSet MissionsSet { get; set; }
        public Mission Mission { get; set; }

        public List<string> OtherWorlds { get; } = new List<string>();
    }
}
