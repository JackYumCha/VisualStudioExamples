using Autofac;
using Jack.DataScience.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VsExample.Data.Entities;
using VsExample.Data.MySQL;
using Xunit;

namespace VsExample.Data.Tests
{
    public class MySQLTests
    {

        private MySQLDataContext BuildContext()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer();
            autoFacContainer.RegisterOptions<MySQLOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<MySQLDataModule>();
            var services = autoFacContainer.ContainerBuilder.Build();
            var mySQL = services.Resolve<MySQLDataContext>();
            return mySQL;
        }

        [Fact(DisplayName = "Generate Data")]
        public void GenerateData()
        {
            var mySQL = BuildContext();

            Random random = new Random();


            var countHusky = (int) Math.Floor( random.NextDouble() * 1000) + 500;
            var countSamoyed = (int)Math.Floor(random.NextDouble() * 1000) + 500;
            var countAlaskan = (int)Math.Floor(random.NextDouble() * 1000) + 500;
            for (int i = 0; i < countHusky; i ++)
            {
                var days = random.Next(365 * 12);
                mySQL.Animals.Add(new AnimalEntity()
                {
                    Age = (int) (days/ 365d),
                    Name = "Husky",
                    DateOfBirth = DateTime.Now.AddDays(-days),
                    IsMale = true,
                    Price = random.NextDouble() * 500d+ 200d
                });
            }

            for (int i = 0; i < countSamoyed; i++)
            {
                var days = random.Next(365 * 12);
                mySQL.Animals.Add(new AnimalEntity()
                {
                    Age = (int)(days / 365d),
                    Name = "Samoyed",
                    DateOfBirth = DateTime.Now.AddDays(-days),
                    IsMale = true,
                    Price = random.NextDouble() * 800d + 400d
                });
            }
                

            for (int i = 0; i < countAlaskan; i++)
            {
                var days = random.Next(365 * 12);
                mySQL.Animals.Add(new AnimalEntity()
                {
                    Age = (int)(days / 365d),
                    Name = "Alaskan Malamute",
                    DateOfBirth = DateTime.Now.AddDays(-days),
                    IsMale = true,
                    Price = random.NextDouble() * 300d + 100d
                });
            }


            mySQL.SaveChanges();

            var animals = mySQL.Animals.ToList();

            Debugger.Break();
        }

        [Fact(DisplayName = "Delete Data")]
        public void DeleteData()
        {
            var mySQL = BuildContext();

            mySQL.Animals.RemoveRange(mySQL.Animals.ToList());

            mySQL.SaveChanges();

            var count = mySQL.Animals.Count();

            Debugger.Break();
        }

        [Fact(DisplayName = "Generate Person Data")]
        public void GeneratePersonData()
        {
            var mySQL = BuildContext();


            Random random = new Random();
            var names = new string[] { "jack", "terry", "tom", "jake", "cherry", "lily" };

            for (int i = 0; i < 5000; i++)
            {
                var days = random.Next(365 * 12);
                mySQL.Persons.Add(new PersonEntity()
                {
                    Age = (int)(days / 365d),
                    Name = names[random.Next(names.Length)],
                    DateOfBirth = DateTime.Now.AddDays(-days),
                    IsMale = random.NextDouble() > 0.5d ? true: false
                });
            }

            mySQL.SaveChanges();
        }



        [Fact(DisplayName = "Generate Friend Data")]
        public void GenerateFriendData()
        {
            var mySQL = BuildContext();


            Random random = new Random();

            int j = 0;
            int count = mySQL.Persons.Count();
            HashSet<string> keys = new HashSet<string>();

            List<FriendShipEntity> list = new List<FriendShipEntity>();
            while (list.Count < 25000)
            {
                int a = random.Next(count);
                int b = random.Next(count);
                while (b == a)
                {
                    b = random.Next(count);
                }

                var friend = new FriendShipEntity()
                {
                    FromPerson = Math.Min(a, b),
                    ToPerson = Math.Max(a, b)
                };

                var key = $"{friend.FromPerson}-{friend.ToPerson}";

                if (keys.Add(key))
                {
                    list.Add(friend);
                }
            }
            mySQL.FriendShip.AddRange(list);
            mySQL.SaveChanges();
        }
    }
}
