using System;
using System.Collections.Generic;
using System.Linq;

namespace BrimstoneMissionGenerator.Models
{
    public class Mission
    {
        public Mission(string name, int? number, int? page, string location, int? randomWorlds, string notes, string intro,
            IEnumerable<string> enemyThemes, IEnumerable<string> rules, IEnumerable<string> objectives, IEnumerable<string> tokens)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Number = number == 0 ? null : number;
            Page = page == 0 ? null : page;
            Locations = new StringCollection((location ?? "").Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList());
            RandomWorlds = randomWorlds;
            Notes = notes;
            Intro = intro;
            EnemyThemes = new StringCollection(enemyThemes.ToList());
            Rules = new StringCollection(rules.ToList());
            Objectives = new StringCollection(objectives.ToList());
            Tokens = new StringCollection(tokens.ToList());
        }

        public StringCollection EnemyThemes { get; }
        public string Intro { get; }
        public StringCollection Locations { get; }
        public string Name { get; }
        public string Notes { get; }
        public int? Number { get; }
        public StringCollection Objectives { get; }
        public int? Page { get; }
        public int? RandomWorlds { get; }
        public StringCollection Rules { get; }
        public StringCollection Tokens { get; }
    }
}