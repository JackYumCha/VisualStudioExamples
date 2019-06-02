using Autofac;
using Jack.DataScience.Common;
using Jack.DataScience.Storage.AzureBlobStorage;
using Jack.DataScience.Storage.SFTP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace VsExamples.Athena.Tests
{
    public class AzureBlobStorageTests
    {
        [Fact(DisplayName = "Create Container")]
        public async void CreateContainer()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var azureBlobStorageAPI = services.Resolve<AzureBlobStorageAPI>();

            var testContainer3 = await azureBlobStorageAPI.GetOrCreateContainer("test-container3");
        }

        [Fact(DisplayName = "Upload File from File System to Container")]
        public async void UploadFileContainer()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var azureBlobStorageAPI = services.Resolve<AzureBlobStorageAPI>();

            var testContainer3 = await azureBlobStorageAPI.GetOrCreateContainer("test-container3");

            var ssh = services.Resolve<SshOptions>();

            var json = JsonConvert.SerializeObject(ssh);
            var baseDirectory = AppContext.BaseDirectory;

            Directory.GetCurrentDirectory();


            var filename = $"{baseDirectory}/ssh-options.json";
            File.WriteAllText(filename, json);

            using(var fileStream = File.OpenRead(filename))
            {
                await azureBlobStorageAPI.Upload("folder/ssh-options.json", fileStream, testContainer3.Name);
            }
        }

        [Fact(DisplayName = "Delete File from Container")]
        public async void DeleteFileFromContainer()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var azureBlobStorageAPI = services.Resolve<AzureBlobStorageAPI>();

            var testContainer3 = await azureBlobStorageAPI.GetOrCreateContainer("test-container3");

            if (await azureBlobStorageAPI.Exists("folder/ssh-options.json", testContainer3.Name))
            {
                await azureBlobStorageAPI.Delete("folder/ssh-options.json", testContainer3.Name);
            }
        }


    }
}
