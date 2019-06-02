using Autofac;
using Jack.DataScience.Common;
using System;
using System.Diagnostics;
using VsExamples.Athena;
using Xunit;

namespace VsExamples.Athena.Tests
{
    public class UploadData
    {
        [Fact(DisplayName = "Upload Data to SFTP")]
        public void UploadDataToSFTP()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var dataGenerator = services.Resolve<DataGenerator>();

            dataGenerator.WriteDataToSFTP();

            Debugger.Break();
        }


        [Fact(DisplayName = "Upload Data to AzureBlobStorage")]
        public async void UploadDataToAzureBlobStorage()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var dataGenerator = services.Resolve<DataGenerator>();

            await dataGenerator.WriteToBlobStorageContainer();

            Debugger.Break();
        }
    }
}
