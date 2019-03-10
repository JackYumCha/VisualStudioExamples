using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace VsExamples.Standard
{
    public class Person: PersonBase, IPerson, IPerson2
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }

        public List<string> EmailAddresses { get; set; }

        public string GivenName
        {
            internal get;
            set;
        }

        public string BuildString()
        {
            throw new NotImplementedException();
        }


        public object generateKey(int i)
        {
            return i > 0 ? (object) "ok" :(object) -1;
        }

        public override string GenerateString()
        {
            object key = generateKey(3);
            IsVisible((dynamic)key);

            return base.GenerateString();
        }

        public bool IsVisible(int index)
        {
            throw new NotImplementedException();
        }

        public bool IsVisible(string name)
        {
            throw new NotImplementedException();
        }

        public override bool Test()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class PersonBase {

        public virtual string GenerateString()
        {

            throw new NotImplementedException();
        }

        public abstract bool Test();

        private class SubPersonClass
        {

        }
    }

    public interface IPerson
    {
        string BuildString();
        bool IsVisible(int index);

    }


    public interface IPerson2
    {
        bool IsVisible(string name);
    }


    public enum GenderEnum
    {
        Male, 
        Female,
        Other
    }

    public static class PersonExtensions
    {
        public static void CalculateAge(this Person person)
        {
            person.Age = (int)Math.Floor((int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(person.DateOfBirth.ToString("yyyyMMdd"))) / 10000d);
        }


        public static string ToJson(this Person person) => JsonConvert.SerializeObject(person);


        public static void Test()
        {
            var p1 = new Person();
            // var p2 = new PersonBase();

        }

        public static void LinqExamples()
        {
            // map filter reduce / Java or JavaScript
            // C#/VB.net 
            /***
             * map = Select
             * filter = Where
             * reduce = Aggregate
             */

            List<Person> persons = new List<Person>()
            {
                new Person()
                {
                    Age = 2
                },
                new Person()
                {
                    Age = 4
                },
                new Person()
                {
                    Age = 3
                },
                new Person()
                {
                    Age = 5
                },
                new Person()
                {
                    Age = 4
                }
            };

            // calculate average
            var ages = persons.Select(p => p.Age).ToList();

            var averageAge1 = persons.Average(p => p.Age);

            var averageAge2 = persons.Select(p => p.Age).Average();

            var firstPerson = persons.First();
            var firstPersonOrDefault = persons.FirstOrDefault(); // return null when list is empty

            var lastPerson = persons.Last();
            var lastPerons2 = persons.LastOrDefault();


            var personsOlderThan3 = persons.Where(p => p.Age > 3).ToList();

            var firstPersonOlderThan3 = persons.Where(p => p.Age > 3).OrderBy(p => p.Age).FirstOrDefault();

            var firstPersonOlderThan3_1 = persons.OrderBy(p => p.Age).FirstOrDefault(p => p.Age > 3);

            foreach (var person in persons)
            {

            }

            for (int i = 0; i < persons.Count(); i++)
            {
                var person = persons[i];
            }

            int j = 0;
            while (j < persons.Count())
            {
                var person = persons[j];
                j++;
            }

            j = 0;
            do
            {
                var person = persons[j];
                j++;
            }
            while (j < persons.Count());

            var sumOfAge1 = persons.Sum(p => p.Age);
            var sumOfAge2 = persons.Select(p => p.Age).Sum();
            var allEmails1 = persons.Aggregate(new List<string>(), (seed, person) =>
            {
                seed.AddRange(person.EmailAddresses);
                return seed;
            }).Distinct().ToList();


            var allEmails2 = persons.Aggregate(new HashSet<string>(), (seed, person) =>
            {
                person.EmailAddresses.ForEach(email => seed.Add(email));
                return seed;
            });

            var allEmails3 = persons.Aggregate(new HashSet<string>(), (seed, person) => seed.AddRange(person.EmailAddresses));
        }

        public static HashSet<T> AddRange<T>(this HashSet<T> hashset, IEnumerable<T> list)
        {
            foreach (var item in list) hashset.Add(item);
            return hashset;
        }


        public static void TestIEnumerable()
        {
            var myList = new MyList<string>()
            {
                "string"
            };

            foreach(var item in myList) { 


            }

            foreach(var randomDouble in GenerateRandomNumber1(200))
            {
                Console.WriteLine($"print number: {randomDouble}");
                var b = randomDouble + 2d;

            }

            var enumerator = GenerateRandomNumber2(200).GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
            }

        }

        public static IEnumerable<double> GenerateRandomNumber1(int count)
        {
            Random random = new Random();

            for(int i = 0; i < count; i++)
            {
                Console.WriteLine("generate number");
                yield return random.NextDouble();
            }
        }
        public static IEnumerable<double> GenerateRandomNumber2(int count)
        {

            List<double> results = new List<double>();
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("generate number");
                results.Add(random.NextDouble());
            }
            return results;
        }


    }


    public class MyList<T> : IEnumerable<T>
    {
        public void Add(string value)
        {

        }
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

}
