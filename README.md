# Lektion 5 – LINQ Exercises

This repository contains exercises completed as part of **Lektion 5**, focusing on working with collections and LINQ in C#.

The exercises start with traditional `List<T>` operations and gradually introduce lambda expressions, predicates, extension methods and LINQ for querying and manipulating collections.

## Topics Covered

- `List<T>` and collection operations
- Lambda expressions
- `Action<T>` and `Predicate<T>`
- `FindAll()` and `FindIndex()`
- Custom extension methods
- `IEnumerable<T>`
- LINQ Method Syntax and Query Expression Syntax
- Filtering with `Where()`
- Projection with `Select()`
- Sorting with `OrderBy()`, `ThenBy()` and their descending variants
- `Count()`, `Take()` and `Distinct()`
- Grouping with `GroupBy()`
- Joining collections with `Join()`
- Deferred execution and materialization

---

## Exercise 5.1 – List Searching

The first exercise focuses on searching and iterating through a `List<int>` without LINQ.

The exercise includes:

- Finding even numbers
- Finding the last number above a specified value
- Finding the index of the last matching number
- Using `ForEach()` with lambda expressions

This exercise introduces lambda expressions together with methods already available on `List<T>`.

---

## Exercise 5.2 – LINQ Basics

The same types of collection operations are performed using LINQ.

Both LINQ syntaxes are explored:

### Method Syntax

```csharp
var result = numbers.Where(n => n % 2 == 0);
```

### Query Expression Syntax

```csharp
var result =
    from n in numbers
    where n % 2 == 0
    select n;
```

The exercises include:

- Finding even integers
- Finding integers containing exactly two digits
- Sorting the results
- Iterating through an `IEnumerable<T>`

An important distinction introduced here is the difference between a `List<T>` and the `IEnumerable<T>` commonly returned by LINQ queries.

---

## Exercise 5.3 – FindAll and Predicate<T>

A `Person` class is introduced containing the following properties:

```csharp
public string Name { get; set; }
public int Age { get; set; }
public int Weight { get; set; }
public int Score { get; set; }
public bool Accepted { get; set; }
```

Person data is loaded from a CSV file into a:

```csharp
List<Person>
```

`FindAll()` is then used to filter persons according to different conditions involving:

- Score
- Weight
- Even values
- Multiple conditions

Example:

```csharp
List<Person> result =
    persons.FindAll(p => p.Score % 2 == 0);
```

### Predicate<T>

`FindAll()` accepts a `Predicate<T>`.

A predicate can be thought of as a function:

```text
Person → bool
```

For example:

```csharp
p => p.Score % 2 == 0
```

returns `true` when the person's score is even and `false` otherwise.

---

## Exercise 5.4 – FindIndex

`FindIndex()` is used together with lambda expressions to locate the first person satisfying a condition.

Examples of conditions include:

```csharp
p => p.Score == 3
```

and:

```csharp
p => p.Age < 10 && p.Score == 3
```

This exercise also demonstrates the importance of understanding the return value of `FindIndex()`.

If no matching element exists, the method returns:

```text
-1
```

This is different from `0`, which represents the first valid index of a list.

---

## Exercise 5.5 – Sorting with List<T>.Sort()

Before sorting with LINQ, `Person` objects can be sorted using `List<T>.Sort()` together with an implementation of:

```csharp
IComparer<Person>
```

For example:

```csharp
people.Sort(new SortByAge());
```

The purpose of this exercise is primarily to demonstrate the difference between traditional sorting and the LINQ approach introduced later.

---

## Exercise 5.6 – Predicates and Extension Methods

A custom extension method is added to `List<Person>`.

This makes it possible to provide the acceptance condition through a lambda expression:

```csharp
persons.SetAcceptedParameter(
    p => p.Score >= 6 && p.Age <= 40);
```

The extension method accepts a:

```csharp
Predicate<Person>
```

Example implementation:

```csharp
public static void SetAcceptedParameter(
    this List<Person> lst,
    Predicate<Person> predicate)
{
    foreach (var person in lst)
    {
        if (predicate(person))
        {
            person.Accepted = true;
        }
    }
}
```

An important distinction is that the predicate itself does not modify the person.

The lambda:

```csharp
p => p.Score >= 6 && p.Age <= 40
```

only determines whether the condition is:

```text
true / false
```

The extension method decides what should happen based on that result.

This allows the same method to be reused with many different conditions.

---

## Exercise 5.7 – Sorting with LINQ

The `Person` collection is sorted again, this time using LINQ instead of implementing an `IComparer<Person>`.

Persons can be sorted according to properties such as:

```text
Score
Age
```

in both ascending and descending order.

Relevant LINQ methods include:

```csharp
OrderBy()
ThenBy()

OrderByDescending()
ThenByDescending()
```

For example:

```csharp
var result = persons
    .OrderBy(p => p.Score)
    .ThenBy(p => p.Age);
```

This demonstrates how LINQ can express sorting directly as part of a query without requiring separate comparer classes.

---

## Exercise 5.8 – LINQ Queries and Projection

The following collection is used:

```csharp
int[] numbers =
{
    34, 8, 56, 31, 79, 150,
    88, 7, 200, 47, 88, 20
};
```

LINQ is used to:

1. Return two-digit integers in ascending order
2. Return two-digit integers in descending order
3. Convert the integers into strings
4. Create strings describing whether numbers are even or uneven

This introduces the use of:

```csharp
Select()
```

for transforming values.

### Where vs Select

`Where()` determines **which elements should remain**:

```csharp
numbers.Where(n => n >= 10 && n <= 99);
```

`Select()` determines **what those elements should become**:

```csharp
numbers.Select(n => n.ToString());
```

The two operations can therefore be combined:

```csharp
var result = numbers
    .Where(n => n >= 10 && n <= 99)
    .OrderBy(n => n)
    .Select(n => n.ToString());
```

---

## Exercise 5.9 – Reset Extension Method

Another extension method is created for `List<Person>`.

Its purpose is to reset:

```csharp
person.Accepted = false;
```

for every person in the collection.

Example:

```csharp
public static void ResetAcceptedParameter(
    this List<Person> lst)
{
    foreach (var person in lst)
    {
        person.Accepted = false;
    }
}
```

It can then be called directly on the list:

```csharp
persons.ResetAcceptedParameter();
```

This reinforces how extension methods can add reusable behaviour to an existing type.

---

## Exercise 5.10 – LINQ Operations

A collection containing 100 random integers is created.

LINQ is then used to:

- Count the number of odd integers
- Count the number of unique integers
- Find the first three odd integers
- Find all unique odd integers

Relevant LINQ methods include:

```csharp
Where()
Count()
Take()
Distinct()
```

One of the important concepts in this exercise is that LINQ operations can be chained together.

For example:

```text
Collection
    ↓
Where
    ↓
Distinct
    ↓
Count
```

Each operation has a specific responsibility and passes its result to the next operation.

---

## Exercise 5.11 – GroupBy

Persons are grouped according to the first character of their name.

For example:

```text
A
├── Anders
└── Anita

B
├── Bent
└── Bo
```

The grouping can be performed using:

```csharp
var personGroups = persons
    .GroupBy(p => p.Name[0])
    .OrderBy(g => g.Key);
```

`GroupBy()` produces groups represented by:

```csharp
IGrouping<char, Person>
```

Each group contains a:

```csharp
group.Key
```

representing the first character, while the group itself contains the matching `Person` objects.

### Avoiding Unnecessary Materialization

An alternative solution could convert every group into an array:

```csharp
persons
    .GroupBy(p => p.Name[0])
    .OrderBy(g => g.Key)
    .Select(g => g.ToArray());
```

However, this is unnecessary if the groups only need to be iterated once.

Keeping the original `IGrouping`:

- Preserves access to `group.Key`
- Avoids creating additional arrays
- Reduces unnecessary memory allocation
- Makes the intention of the code clearer

### Complexity

If:

```text
n = number of persons
k = number of groups
```

the approximate complexity is:

```text
GroupBy:  O(n)
OrderBy:  O(k log k)

Total:    O(n + k log k)
```

Since the number of groups is small when grouping by the first letter of a name, the operation will generally be dominated by `O(n)`.

---

## Exercise 5.12 – Inner Join

The final exercise uses two CSV datasets:

```text
data1.csv
data2.csv
```

The goal is to find persons with matching names in both collections.

This corresponds conceptually to an SQL:

```sql
INNER JOIN
```

Using LINQ:

```csharp
var joinedPersons = persons.Join(
    persons2,
    p1 => p1.Name,
    p2 => p2.Name,
    (p1, p2) => new
    {
        Person1 = p1,
        Person2 = p2
    });
```

The four important parts of `Join()` are:

```text
persons
   ↓
First collection

persons2
   ↓
Second collection

p1 => p1.Name
   ↓
Key from the first collection

p2 => p2.Name
   ↓
Key from the second collection
```

When the keys match, the result selector determines what should be returned.

The equivalent SQL concept would be:

```sql
SELECT *
FROM Persons p1
INNER JOIN Persons2 p2
    ON p1.Name = p2.Name;
```

The resulting sequence can then be iterated:

```csharp
foreach (var joined in joinedPersons)
{
    Console.WriteLine(
        $"Match found: {joined.Person1} and {joined.Person2}");
}
```

### ToList().ForEach() vs foreach

The same operation could be written as:

```csharp
persons.Join(...)
    .ToList()
    .ForEach(...);
```

This works, but `ToList()` materializes the entire result into a new list before it is processed.

If the result only needs to be iterated once, keeping the result as an `IEnumerable<T>` and using `foreach` avoids the unnecessary result list.

---

## Deferred Execution

One important concept encountered while working with LINQ is **deferred execution**.

For example:

```csharp
var result = persons
    .Where(p => p.Score >= 6)
    .OrderBy(p => p.Name);
```

does not necessarily process the complete collection immediately.

Instead, the LINQ query describes how the result should be produced.

The query is evaluated when it is enumerated:

```csharp
foreach (var person in result)
{
    Console.WriteLine(person);
}
```

Methods such as:

```csharp
ToList()
ToArray()
```

materialize the sequence.

This means that the query is evaluated and its result is stored in memory.

A useful general rule is:

```text
Only need to iterate the result?
→ Keep IEnumerable<T>

Need a stored snapshot or List<T> functionality?
→ Consider ToList()

Specifically need an array?
→ Consider ToArray()
```

---

## LINQ as a Pipeline

A useful way of thinking about LINQ is as a sequence of operations:

```text
Source Collection
       ↓
     Where
       ↓
    OrderBy
       ↓
 GroupBy / Join
       ↓
     Select
       ↓
Iteration / Materialization
```

Not every query requires every operation.

Each method should have a reason for being part of the query.

---

## Key Takeaways

The exercises demonstrate a progression from traditional collection operations towards LINQ:

```text
List<T> methods
       ↓
Lambda expressions
       ↓
Predicates
       ↓
Extension methods
       ↓
LINQ
       ↓
Filtering
Sorting
Projection
Grouping
Joining
```

Some of the main concepts I worked with were:

- Using lambda expressions to describe behaviour
- Passing conditions through `Predicate<T>`
- Creating custom extension methods
- Understanding `IEnumerable<T>`
- Filtering collections with `Where()`
- Transforming data with `Select()`
- Sorting using `OrderBy()` and `ThenBy()`
- Finding unique elements using `Distinct()`
- Grouping objects using `GroupBy()`
- Joining collections using `Join()`
- Understanding the relationship between LINQ joins and SQL joins
- Avoiding unnecessary materialization with `ToList()` and `ToArray()`
- Considering readability as well as performance when writing LINQ queries

---

## Technologies

- C#
- .NET
- LINQ
- CSV
- JetBrains Rider / Visual Studio
