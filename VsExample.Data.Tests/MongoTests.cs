using Autofac;
using Jack.DataScience.Common;
using Jack.DataScience.Data.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using VsExample.Data.Entities;
using VsExample.Data.Mongo;
using Xunit;

namespace VsExample.Data.Tests
{
    public class MongoTests
    {
        private IContainer BuildContainer()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer();
            autoFacContainer.RegisterOptions<MongoOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<MongoModule>();
            return autoFacContainer.ContainerBuilder.Build();
        }

        [Fact(DisplayName = "Connect to MongoDB")]
        public void ConnectToMongoDB()
        {
            var services = BuildContainer();

            var mongo = services.Resolve<MongoContext>();

            var colPersonEntity = mongo.Collection<PersonEntity>();

            colPersonEntity.InsertOne(new PersonEntity() {
               Id = new Random().Next(500000),
               Age = 20,
               DateOfBirth = DateTime.Now.AddYears(-20),
               IsMale = true,
               Name = "Tom"
            });

            Debugger.Break();
        }


        [Fact(DisplayName = "Generate Person Data")]
        public void GeneratePersonData()
        {
            var services = BuildContainer();

            var mongo = services.Resolve<MongoContext>();
            Random random = new Random();
            var names = new string[] { "jack", "terry", "tom", "jake", "cherry", "lily" };

            List<Person> persons = new List<Person>();
            for (int i = 0; i < 5000; i++)
            {
                var days = random.Next(365 * 12);
                persons.Add(new Person()
                {
                    _id = Guid.NewGuid().ToString(),
                    Age = (int)(days / 365d),
                    Name = names[random.Next(names.Length)],
                    DateOfBirth = DateTime.Now.AddDays(-days),
                    IsMale = random.NextDouble() > 0.5d ? true : false,
                });
            }

            persons.ForEach(p =>
            {
                int numberTeamMembers = random.Next(30);
                var memberIds = new HashSet<string>();
                for(int i = 0; i < numberTeamMembers; i++)
                {
                    int index = random.Next(persons.Count);
                    memberIds.Add(persons[index]._id);
                    persons[index].ReportsTo = p._id;
                }
                p.TeamMembers = memberIds.ToList();
            });

            mongo.Collection<Person>().InsertMany(persons);
        }

        [Fact(DisplayName = "Find TeamMembers' TeamMembers")]
        public void MongoDBJoinQuery()
        {
            var services = BuildContainer();

            var mongo = services.Resolve<MongoContext>();

            var colPerson = mongo.Collection<Person>();


            var teamMemberIds = colPerson.AsQueryable()
                .Where(p => p._id == "f66fcec3-9029-4824-ad0d-1c3e22046dff")
                .Select(p => p.TeamMembers)
                .ToList()
                .Aggregate(new HashSet<string>(), (hs, ids) =>
                {
                    ids.ForEach(id => hs.Add(id));
                    return hs;
                });

            var teamMembers = colPerson.AsQueryable()
                .Where(p => teamMemberIds.Contains(p._id))
                .ToList();

            colPerson.Indexes.CreateOne(new CreateIndexModel<Person>(Builders<Person>.IndexKeys.Hashed(person => person.ReportsTo)));


            var list = colPerson.AsQueryable().ToList();

            var listAgeBelow8 = colPerson.AsQueryable()
                .Where(p => p.Age <= 8)
                .ToList();

            listAgeBelow8.ForEach(p => p.Age += 20);

            colPerson.BulkReplaceEach(listAgeBelow8);

            colPerson.UpdateMany(Builders<Person>.Filter.Where(p => p.Age >= 20),
                Builders<Person>.Update
                .Set(p => p.Age, 18));

             var teamMembersByJoin = colPerson.AsQueryable()
                .Where(p => p._id == "db8157fc-ab90-4b16-9bf9-dc4980854f52")
                .Join(colPerson.AsQueryable(), p => p._id, k => k.ReportsTo,
                    (p, k) => new PersonTeamMembers(){ Manager = new Person()
                    {
                        _id = p._id,
                        ReportsTo = p.ReportsTo
                    }, DirectMember = new Person()
                    {
                        _id = k._id,
                        ReportsTo = k.ReportsTo
                    }
                    })
                    
                //.Join(colPerson.AsQueryable(), mt => mt.DirectMember._id, k => k.ReportsTo,
                //(mt, k) => new PersonTeamMembers() { Manager = mt.Manager, DirectMember = mt.DirectMember, IndirectMember = k })
                .ToList();

            Debugger.Break();
        }

    }

    public class PersonTeamMembers: DocumentBase
    {
        public Person Manager { get; set; }
        public Person DirectMember { get; set; }
        public Person IndirectMember { get; set; }

    }
}
