using System.Collections.Generic;

namespace BrimstoneMissionGenerator.Models
{
    public class MissionPicker
    {
        public Product MissionsSet { get; set; }
        public Mission Mission { get; set; }

        public List<string> OtherWorlds { get; } = new List<string>();
    }
}



