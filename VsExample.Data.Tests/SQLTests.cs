using Autofac;
using Jack.DataScience.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VsExample.Data.Entities;
using VsExample.Data.MySQL;
using VsExample.Data.PostgresSQL;
using VsExample.Data.SQLServer;
using Xunit;

namespace VsExample.Data.Tests
{
    public class SQLTests
    {

        private IContainer BuildContainer()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer();
            autoFacContainer.RegisterOptions<MySQLOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<MySQLDataModule>();
            autoFacContainer.RegisterOptions<PostgresSQLOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<PostgresSQLModule>();
            autoFacContainer.RegisterOptions<SQLServerOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<SQLServerModule>();
            return autoFacContainer.ContainerBuilder.Build();
        }

        [Fact(DisplayName = "Generate Data")]
        public void GenerateData()
        {
            var services = BuildContainer();
            var mySQL = services.Resolve<MySQLDataContext>();

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
            var services = BuildContainer();
            var mySQL = services.Resolve<MySQLDataContext>();

            mySQL.Animals.RemoveRange(mySQL.Animals.ToList());

            mySQL.SaveChanges();

            var count = mySQL.Animals.Count();

            Debugger.Break();
        }

        [Fact(DisplayName = "Generate Person Data")]
        public void GeneratePersonData()
        {
            var services = BuildContainer();
            var mySQL = services.Resolve<MySQLDataContext>();
            var postgresSQL = services.Resolve<PostgresSQLDataContext>();
            var sqlServer = services.Resolve<SQLServerDataContext>();


            Random random = new Random();
            var names = new string[] { "jack", "terry", "tom", "jake", "cherry", "lily" };

            List<PersonEntity> persons = new List<PersonEntity>();
            for (int i = 0; i < 5000; i++)
            {
                var days = random.Next(365 * 12);
                persons.Add(new PersonEntity()
                {
                    Age = (int)(days / 365d),
                    Name = names[random.Next(names.Length)],
                    DateOfBirth = DateTime.Now.AddDays(-days),
                    IsMale = random.NextDouble() > 0.5d ? true: false
                });
            }

            persons.ForEach(p => p.Id = 0);
            mySQL.Persons.AddRange(persons);
            mySQL.SaveChanges();

            persons.ForEach(p => p.Id = 0);
            postgresSQL.Persons.AddRange(persons);
            postgresSQL.SaveChanges();

            persons.ForEach(p => p.Id = 0);
            sqlServer.Persons.AddRange(persons);
            sqlServer.SaveChanges();
        }



        [Fact(DisplayName = "Generate Friend Data")]
        public void GenerateFriendData()
        {
            var services = BuildContainer();
            var mySQL = services.Resolve<MySQLDataContext>();
            var postgresSQL = services.Resolve<PostgresSQLDataContext>();
            var sqlServer = services.Resolve<SQLServerDataContext>();


            Random random = new Random();

            int j = 0;
            int count = mySQL.Persons.Count();
            HashSet<string> keys = new HashSet<string>();

            List<FriendShipEntity> list = new List<FriendShipEntity>();
            while (list.Count < 25000)
            {
                int a = random.Next(count) + 1;
                int b = random.Next(count) + 1;
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
            list.ForEach(f => f.Id = 0);
            mySQL.FriendShip.AddRange(list);
            mySQL.SaveChanges();
            list.ForEach(f => f.Id = 0);
            postgresSQL.FriendShip.AddRange(list);
            postgresSQL.SaveChanges();
            list.ForEach(f => f.Id = 0);
            sqlServer.FriendShip.AddRange(list);
            sqlServer.SaveChanges();
        }


        [Fact(DisplayName = "Run CTE in C#")]
        public void RunCTEinCSharp()
        {
            var services = BuildContainer();
            var mySQL = services.Resolve<MySQLDataContext>();
            var postgresSQL = services.Resolve<PostgresSQLDataContext>();
            var sqlServer = services.Resolve<SQLServerDataContext>();

            var id = 13;

            var friendsMySQL = mySQL.FriendShip.Where(f => f.FromPerson == id)
                .Join(mySQL.Persons, f => f.ToPerson, p => p.Id, (f,p)=> p).ToList();

            var numberOfRounds = 3;

            var sqlMySQL = $@"
WITH RECURSIVE cte (Id, FromPerson, ToPerson, Round) 
AS (
      SELECT {nameof(FriendShipEntity.Id)}, {nameof(FriendShipEntity.FromPerson)}, {nameof(FriendShipEntity.ToPerson)}, 1 as Round
      From vsexamples.{nameof(MySQLDataContext.FriendShip)}
      WHERE {nameof(FriendShipEntity.FromPerson)} = {id}
      UNION ALL
      SELECT f.{nameof(FriendShipEntity.Id)} as {nameof(FriendShipEntity.Id)}, f.{nameof(FriendShipEntity.FromPerson)} as {nameof(FriendShipEntity.FromPerson)}, f.{nameof(FriendShipEntity.ToPerson)} as {nameof(FriendShipEntity.ToPerson)}, cte.Round + 1 as Round
      FROM vsexamples.{nameof(MySQLDataContext.FriendShip)} f
      INNER JOIN  cte
      ON f.{nameof(FriendShipEntity.FromPerson)} = cte.{nameof(FriendShipEntity.ToPerson)} And cte.Round < {numberOfRounds}
    )
SELECT * 
FROM cte";

            var freindsRecursiveMySQL = mySQL.FriendShip.FromSql(sqlMySQL)
                .Join(mySQL.Persons, f => f.ToPerson, p => p.Id, (f, p) => p)
                .ToList();

            var freindsRecursiveMySQLMappings = mySQL.FriendShip.FromSql(sqlMySQL)
                .Join(mySQL.Persons, f => f.ToPerson, p => p.Id, (f, p) => new { FriendShip = f, Person = p })
                .Join(mySQL.Persons, map => map.FriendShip.FromPerson, p => p.Id, (map, p) => new {From = p, To =map.Person })
                .ToList();

            // pagination

            var pageIndex = 2;
            var numberPerPage = 50;

            var count = mySQL.FriendShip.FromSql(sqlMySQL).Count();
            var numberOfPages = (int) Math.Ceiling(count / (double)numberPerPage);


            var freindsRecursiveMySQLMappingsPaged = mySQL.FriendShip.FromSql(sqlMySQL)
                .Join(mySQL.Persons, f => f.ToPerson, p => p.Id, (f, p) => new { FriendShip = f, Person = p })
                .Join(mySQL.Persons, map => map.FriendShip.FromPerson, p => p.Id, (map, p) => new { From = p, To = map.Person })
                .Skip(pageIndex * numberPerPage)
                .Take(numberPerPage)
                .ToList();



            var sqlPostgreSQL = $@"
WITH RECURSIVE cte (""Id"", ""FromPerson"", ""ToPerson"", ""Round"") 
AS (
      SELECT ""{nameof(FriendShipEntity.Id)}"", ""{nameof(FriendShipEntity.FromPerson)}"", ""{nameof(FriendShipEntity.ToPerson)}"", 1 as ""Round""
      From vsexamples.public.""{nameof(MySQLDataContext.FriendShip)}""
      WHERE ""{nameof(FriendShipEntity.FromPerson)}"" = {id}
      UNION ALL
      SELECT f.""{nameof(FriendShipEntity.Id)}"" as ""{nameof(FriendShipEntity.Id)}"", f.""{nameof(FriendShipEntity.FromPerson)}"" as ""{nameof(FriendShipEntity.FromPerson)}"", f.""{nameof(FriendShipEntity.ToPerson)}"" as ""{nameof(FriendShipEntity.ToPerson)}"", cte.""Round"" + 1 as ""Round""
      FROM vsexamples.public.""{nameof(MySQLDataContext.FriendShip)}"" f
      INNER JOIN  cte
      ON f.""{nameof(FriendShipEntity.FromPerson)}"" = cte.""{nameof(FriendShipEntity.ToPerson)}"" And cte.""Round"" < {numberOfRounds}
    )
SELECT * 
FROM cte";

            var freindsRecursivePostgreSQLMappingsPaged = postgresSQL.FriendShip.FromSql(sqlPostgreSQL)
                .Join(mySQL.Persons, f => f.ToPerson, p => p.Id, (f, p) => new { FriendShip = f, Person = p })
                .Join(mySQL.Persons, map => map.FriendShip.FromPerson, p => p.Id, (map, p) => new { From = p, To = map.Person })
                .Skip(pageIndex * numberPerPage)
                .Take(numberPerPage)
                .ToList();

            Debugger.Break();
        }

    }

    
}
