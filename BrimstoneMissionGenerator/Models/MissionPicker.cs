using System.Collections.Generic;

namespace BrimstoneMissionGenerator.Models
{
    public class MissionPicker
    {
        public MissionsSet MissionsSet { get; set; }
        public MissionsSetMission Mission { get; set; }

        public List<string> OtherWorlds { get; } = new List<string>();
    }
}



