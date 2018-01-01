using System;
using System.Linq;

namespace BrimstoneMissionGenerator.Models
{
    public class Mission
    {
        public Mission(string name, int? number, int? page, string location, int? randomWorlds, string notes, string intro)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Number = number == 0 ? null : number;
            Page = page == 0 ? null : page;
            Locations = new StringCollection((location ?? "").Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList());
            RandomWorlds = randomWorlds;
            Notes = notes;
            Intro = intro;
        }

        public string Intro { get; }
        public StringCollection Locations { get; }
        public string Name { get; }
        public string Notes { get; }
        public int? Number { get; }
        public int? Page { get; }
        public int? RandomWorlds { get; }
    }
}