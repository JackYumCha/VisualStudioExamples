using Autofac;
using Jack.DataScience.Common;
using Jack.DataScience.Data.AWSDynamoDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace VsExamples.Athena.Tests
{
    public class AWSDynamoDBTests
    {
        [Fact(DisplayName = "Write Data Into DyanmoDB")]
        public async void WriteDataIntoDynamoDB()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var awsDynamoAPI = services.Resolve<AWSDynamoAPI>();

            await awsDynamoAPI.WriteItem(new Customer()
            {
                key = "test02",
                Name = "John Smith",
                DateOfBirth = new DateTime(1995, 12, 12),
                Credit = 600d,
                CustomerId = 4324235,
                Phone = "0443636862"
            });
        }

        [Fact(DisplayName = "Read Data From DyanmoDB")]
        public async void ReadDataFromDynamoDB()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var awsDynamoAPI = services.Resolve<AWSDynamoAPI>();

            var customer = await awsDynamoAPI.ReadItem<Customer>("test02");

            Debugger.Break();
        }
    }

    public class Customer
    {
        public string key { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CustomerId { get; set; }
        public double Credit { get; set; }
    }
}
