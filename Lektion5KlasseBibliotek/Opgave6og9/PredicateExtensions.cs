using Lektion5KlasseBibliotek.Opgave3;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lektion5KlasseBibliotek.Opgave6
{
    public static class PredicateExtensions
    {
        public static void SetAcceptedParameter(this List<Person> lst, Predicate<Person> predicate)
        {
            foreach (var person in lst)
            {
                if (predicate(person))
                {
                    person.Accepted = true;
                }
            }
        }

        public static void resetAcceptedParameter(this List<Person> lst)
        {
            foreach (var person in lst)
            {
                person.Accepted = false;
            }
        }
    }
}
