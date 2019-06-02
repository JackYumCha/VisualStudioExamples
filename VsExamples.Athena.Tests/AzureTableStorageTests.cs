using Autofac;
using Jack.DataScience.Common;
using Jack.DataScience.Data.AzureTableStorage;
using Jack.DataScience.Storage.SFTP;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xunit;

namespace VsExamples.Athena.Tests
{
    public class AzureTableStorageTests
    {

        [Fact(DisplayName = "Write customers into Table Storage")]
        public async void WriteCustomers()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var azureTableStorageAPI = services.Resolve<AzureTableStorageAPI>();

            await azureTableStorageAPI.Put(new CustomerEntity()
            {
                CustomerType = "Student",
                CustomerName = "John Adams",
                Credit = 200d,
                CustomerId = 10082,
                DateOfBirth = new DateTime(1995, 12, 25),
                PhoneNumber = "0444333222"
            });

            await azureTableStorageAPI.Put(new CustomerEntity()
            {
                CustomerType = "Teacher",
                CustomerName = "John Smith",
                Credit = 400d,
                CustomerId = 10034,
                DateOfBirth = new DateTime(1985, 12, 25),
                PhoneNumber = "0444355222"
            });

        }

        [Fact(DisplayName = "Get customers from Table Storage")]
        public async void GetCustomers()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var azureTableStorageAPI = services.Resolve<AzureTableStorageAPI>();

            var student = await azureTableStorageAPI.Get<CustomerEntity>("Student", "John Adams");

            var teacher = await azureTableStorageAPI.Get<CustomerEntity>("Teacher", "John Smith");

            Debugger.Break();

        }

        [Fact(DisplayName = "Replace and Delete customers")]
        public async void ReplaceAndDeleteCustomers()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var azureTableStorageAPI = services.Resolve<AzureTableStorageAPI>();

            await azureTableStorageAPI.Upsert<CustomerEntity>(new CustomerEntity()
            {
                CustomerType = "Student",
                CustomerName = "John Adams",
                Credit = 600d,
                CustomerId = 10044,
                DateOfBirth = new DateTime(1995, 12, 25),
                PhoneNumber = "0444666222"
            });

            var teacher = await azureTableStorageAPI.Delete<CustomerEntity>(new CustomerEntity()
            {
                CustomerType = "Teacher",
                CustomerName = "John Smith",
                ETag = "*"
            });

            Debugger.Break();

        }

    }

    public class CustomerEntity: TableEntity
    {
        public string CustomerType
        {
            get => PartitionKey;
            set => PartitionKey = value;
        }

        public string CustomerName
        {
            get => RowKey;
            set => RowKey = value;
        }

        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public double Credit { get; set; }
        public int CustomerId { get; set; }
    }
}
