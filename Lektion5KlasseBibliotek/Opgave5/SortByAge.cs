using Lektion5KlasseBibliotek.Opgave3;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lektion5KlasseBibliotek.Opgave5
{
    public class SortByAge : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x is null) return -1;
            if (y is null) return 1;
            return x.Age.CompareTo(y.Age);
        }
    }
}
