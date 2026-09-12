// Opgave 1, arbejd med en liste af ints.

using Lektion5KlasseBibliotek.Opgave3;
using Lektion5KlasseBibliotek.Opgave5;
using Lektion5KlasseBibliotek.Opgave6;

List<int> num = new List<int> { 2, 2, 4, 4, 7, 7, 7, 10, 10, 12 };
Console.WriteLine("Alle lige tal");
List<int> evenNumbers = num.FindAll(x => x % 2 == 0);
evenNumbers.ForEach(x => Console.WriteLine(x));
Console.WriteLine("Alle tal større end eller lig med 15");
List<int> greaterThan = num.FindAll(x => x >= 15);
greaterThan.ForEach(x => Console.WriteLine(x));

// Opgave 2, lav søgninger med LINQ, både query methods og query expressions.
List<int> numbEvenLINQ = num.Where(x => x % 2 == 0).ToList();
Console.WriteLine("Nu med LINQ");
numbEvenLINQ.ForEach(x => Console.WriteLine(x));
List<String> numbGreaterThanLINQ = num.Select(x => x.ToString()).Where(x => int.Parse(x) >= 10).ToList();
numbGreaterThanLINQ.ForEach(x => Console.WriteLine(x));

// Opgave 3, lav en liste af Person objekter og filtrer dem med LINQ.

List<Person> persons = new List<Person>();

Opgave3(persons);

static void Opgave3(List<Person> list)
{
    try
    {
        list.AddRange(Person.ReadCSVFile("C:\\Users\\svall\\source\\repos\\Lektion5LINQ\\Lektion5KlasseBibliotek\\Assets\\data1.csv"));
    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception: " + ex.Message);
        Console.WriteLine("Wrong pathing?"); Console.WriteLine(ex.StackTrace);
    }
}

//foreach (var p in persons)
//{
//    Console.WriteLine(p);
//}

List<Person> lowScorePersons = persons.FindAll(p => p.Score < 2);
Console.WriteLine("Personer med score under 2");
lowScorePersons.ForEach(p => Console.WriteLine(p));
Console.WriteLine("Personer med lige score");
List<Person> evenScorePersons = persons.FindAll(p => p.Score % 2 == 0);
evenScorePersons.ForEach(p => Console.WriteLine(p));
Console.WriteLine("Personer med lige score og vægt over 60");
List<Person> evenScoreAndWeightOver = persons.FindAll(p => p.Score % 2 == 0 && p.Weight > 60);
evenScoreAndWeightOver.ForEach(p => Console.WriteLine(p));
Console.WriteLine("Personer med vægt delelig med 3");
List<Person> vælgtDeleligtMed = persons.FindAll(p => p.Weight % 3 == 0);
vælgtDeleligtMed.ForEach(p => Console.WriteLine(p));

// Opgave 4, Using FindIndex.

int person1Index = persons.FindIndex(p => p.Score == 3);
Console.WriteLine("Index of person with score 3: " + person1Index + " If none is found -1");
Console.WriteLine("Person: " + persons[person1Index]);

int person2Index = persons.FindIndex(p => p.Score == 3 && p.Age < 10);
Console.WriteLine("Index of person with score 3 and age under 10: " + person2Index + " If none is found -1");
Console.WriteLine("Person: " + (person2Index != -1 ? persons[person2Index] : "None found")); //Brug toString() normalt.

List<int> persons2Index = persons.FindAll(p => p.Score == 3 && p.Age < 10)
    .Select(p => persons.IndexOf(p)).ToList();

foreach (var index in persons2Index)
{
    Console.WriteLine("List of Persons using list of index: " + persons[index]);
}

int person3IndexFejlfindning = persons.FindIndex(p => p.Score == 3 && p.Age < 8);
Console.WriteLine("Person under age of 8 with score of 3, -1 if none is found.");
Console.WriteLine(person3IndexFejlfindning);

// Opgave 5, Sorting with List<T> .Sort()

persons.Sort(new SortByAge());

// Opgave 6, Using PredicateExtensions to set Accepted property based on a condition.

persons.SetAcceptedParameter(p => p.Score >= 6 && p.Age <= 40);
List<Person> acceptedPersons = persons.FindAll(p => p.Accepted);
foreach (var p in acceptedPersons)
{
    Console.WriteLine("Accepted Person: " + p);
}

// Opgave 7, Using LINQ to sort after Score and Age. 
List<Person> sortedPersonsAscending = persons.OrderBy(p => p.Score).ThenBy(p => p.Age).ToList();
List<Person> sortedPersonsDescending = persons.OrderByDescending(p => p.Score).ThenByDescending(p => p.Age).ToList();

Console.WriteLine(sortedPersonsAscending.Count + " Persons sorted ascending by Score and Age:");
foreach (var p in sortedPersonsAscending)
{
    Console.WriteLine(p);
}
Console.WriteLine(sortedPersonsDescending.Count + " Persons sorted descending by Score and Age:");
foreach (var p in sortedPersonsDescending)
{
    Console.WriteLine(p);
}

// Opgave 8. Using LINQ on integers. Return all two digit numbers, both sorting orders.
//              Then return strings instead of numbers, and add even or uneven to the string.

int[] numbers = { 34, 8, 56, 31, 79, 150, 88, 7, 200, 47, 88, 20 };
// Delopgave 1 og 2
IEnumerable<int> twoDigitNumbersAscending = numbers.Where(n => n >= 10 && n <= 99).OrderBy(n => n);
IEnumerable<int> twoDigitNumbersDescending = numbers.Where(n => n >= 10 && n <= 99).OrderByDescending(n => n);
// Delopgave 3
IEnumerable<String> stringTwoDigitNumbersAscending = twoDigitNumbersAscending.Select(n => n.ToString());
IEnumerable<String> stringTwoDigitNumbersDescending = twoDigitNumbersDescending.Select(n => n.ToString());
// Delopgave 4.
IEnumerable<String> stringTwoDigitNumbersEvenAddition = twoDigitNumbersAscending.Select(n => n % 2 == 0 ? " even" : " uneven");
IEnumerable<String> stringTwoDigitNumbersUnevenAddition = twoDigitNumbersDescending.Select(n => n % 2 == 0 ? " even" : " uneven");
// Exception.
IEnumerable<String> stringTwoDigitNumbersEvenAdditionString = stringTwoDigitNumbersAscending.Select(n => int.Parse(n) % 2 == 0 ? n + " even" : n + " uneven");
foreach (var n in stringTwoDigitNumbersEvenAdditionString)
{
    Console.WriteLine(n);
}

// Opgave 9. Using LINQ Reset extension method to reset the Accepted property of all Person objects in the list.

persons.FindAll(p => p.Accepted).ForEach(p => Console.WriteLine("Accepted Person before reset: " + p));

persons.resetAcceptedParameter();

persons.FindAll(p => p.Accepted).ForEach(p => Console.WriteLine("Accepted Person after reset: " + p));

// Opgave 10. Using LINQ over a list of 100 integers. Find amount of even and uneven numbers, first 3 uneven numbers and find all unique uneven numbers.

int randomNumber = 0;
List<int> nummers = new List<int>();

for (int i = 0; i < 100; i++)
{
    randomNumber = new Random().Next(1, 1000);
    nummers.Add(randomNumber);
}

IEnumerable<int> even100Numbers = nummers.Where(n => n % 2 == 0);
IEnumerable<int> uneven100Numbers = nummers.Where(n => n % 2 != 0);
IEnumerable<int> first3Uneven100Numbers = uneven100Numbers.Take(3);
IEnumerable<int> uniqueUneven100Numbers = uneven100Numbers.Distinct();

// Opgave 11. Brug LINQ til at gruppere alle personerne efter først bogstav I deres navn, 
//              Anders og Anita skal være i samme gruppe, og Bent og Bo skal være i samme gruppe.
// Løsningen med IGrouping<char, Person> er mere effektiv end at bruge Person[] fordi den har en lavere konstant køretid.
// Ifølge Big O notation, er køretiden stortset den samme, men konstanten er lavere, hvilket kunne betyde i en database
//      at det ville tage længere tid at hente dataene, hvis man bruger Person[] i stedet for IGrouping<char, Person>.

/* Runtime: O(n + n log n + n)
 * GroupBy() - Groups the elements of a sequence according to a specified key selector function.
 * OrderBy() - Sorts the elements of a sequence in ascending order according to a key.
 * Select() - Projects each element of a sequence into a new form.
 */
IEnumerable<Person[]> personGrupperingslist = 
    persons.GroupBy(p => p.Name[0])
    .OrderBy(g => g.Key)
    .Select(g => g.ToArray());

// Runtime: O(n + n log n)
IEnumerable<IGrouping<char, Person>> personGrupper =
    persons
        .GroupBy(p => p.Name[0])
        .OrderBy(g => g.Key);  

//foreach (var gruppe in personGrupperingslist)
//{
//    Console.WriteLine("Group: " + gruppe[0].Name[0]);
//    foreach (var p in gruppe)
//    {
//        Console.WriteLine(p);
//    }
//    Console.WriteLine();
//}

foreach (var gruppe in personGrupper)
{
    Console.WriteLine("Group: " + gruppe.Key);
    foreach (var p in gruppe)
    {
        Console.WriteLine(p);
    }
    Console.WriteLine();
}

// Opgave 12. Inner join på data1.cv og data2.cv, hvor vi skal finde alle personer med samme navn i begge
//             filer, og udskrive dem i konsollen. Brug LINQ.   

List<Person> persons2 = Person.ReadCSVFile("C:\\Users\\svall\\source\\repos\\Lektion5LINQ\\Lektion5KlasseBibliotek\\Assets\\data2.csv");

persons.Join(persons2, p1 => p1.Name, p2 => p2.Name, (p1, p2) => new { Person1 = p1, Person2 = p2 })
    .ToList()
    .ForEach(joined => Console.WriteLine($"Match found: {joined.Person1} and {joined.Person2}"));

// Alternativ version: Runtime: O(n+m+r) hvor n er antallet af personer i persons,
//      m er antallet af personer i persons2, og r er antallet af matches fra join.
// Denne version undgår at materialisere join-resultatet i en List,
// da resultatet kun skal gennemløbes én gang og udskrives.
// Det giver mindre unødvendig memory-allokering og gør intentionen tydeligere.

var joinedPersons = persons.Join(
    persons2,
    p1 => p1.Name,
    p2 => p2.Name,
    (p1, p2) => new
    {
        Person1 = p1,
        Person2 = p2
    });

foreach (var joined in joinedPersons)
{
    Console.WriteLine(
        $"Match found: {joined.Person1} and {joined.Person2}");
}