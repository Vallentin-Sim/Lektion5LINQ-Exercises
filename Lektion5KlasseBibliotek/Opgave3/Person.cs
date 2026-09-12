using System.Runtime.CompilerServices;

namespace Lektion5KlasseBibliotek.Opgave3
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public int Score { get; set; }
        public bool Accepted { get; set; }

        public Person(String data) 
        { 
            var L = data.Split(';');
            this.Name = L[0];
            this.Age = int.Parse(L[1]);
            this.Weight = int.Parse(L[2]);
            this.Score = int.Parse(L[3]);
            this.Accepted = false;
        }

        public static List<Person> ReadCSVFile(string filename)
        {
            List<Person> list = new List<Person>();
            using (var file = new StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var p = new Person(line);
                    list.Add(p);
                    //Console.WriteLine(p);
                }
            }
            return list;
        }

        public override string ToString()
        {
            return "Name: " + Name + ", Age: " + Age + ", Weight: " + Weight + ", Score: " + Score + ", Accepted: " + Accepted;
        }
    }
}
