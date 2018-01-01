using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}