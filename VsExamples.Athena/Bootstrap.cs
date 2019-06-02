using Jack.DataScience.Common;
using Jack.DataScience.Storage.AWSS3;
using Jack.DataScience.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Jack.DataScience.Storage.SFTP;
using Jack.DataScience.Storage.AzureBlobStorage;
using Autofac;
using Jack.DataScience.Data.AWSAthena;
using Jack.DataScience.Data.AzureTableStorage;
using Jack.DataScience.Data.AWSDynamoDB;

namespace VsExamples.Athena
{
    public static class Bootstrap
    {
        public static AutoFacContainer Setup(this AutoFacContainer autoFacContainer)
        {
            autoFacContainer.RegisterOptions<SshOptions>();
            autoFacContainer.RegisterOptions<AWSS3Options>();
            autoFacContainer.RegisterOptions<AzureBlobStorageOptions>();
            autoFacContainer.RegisterOptions<AzureTableStorageOptions>();
            autoFacContainer.RegisterOptions<AWSDynamoDBOptions>();
            autoFacContainer.RegisterOptions<AWSAthenaOptions>();
            autoFacContainer.RegisterOptions<AthenaOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<VsExamplesAthenaModule>();
            autoFacContainer.ContainerBuilder.RegisterModule<AzureBlobStorageModule>();
            autoFacContainer.ContainerBuilder.RegisterModule<AzureTableStorageModule>();
            autoFacContainer.ContainerBuilder.RegisterModule<AWSDynamoDBModule>();
            return autoFacContainer;
        }
    }
}
