using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BrimstoneMissionGenerator.Models
{
    public class StringCollection : ReadOnlyCollection<string>
    {
        /// <summary>Initializes a new instance of the <see cref="StringCollection`1" /> class that is a read-only wrapper around the specified list.</summary>
        /// <param name="list">The list to wrap.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="list" /> is null.</exception>
        public StringCollection(IList<string> list) : base(list)
        {
        }

        public static new StringCollection Empty { get; } = new StringCollection(new List<string>());

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
