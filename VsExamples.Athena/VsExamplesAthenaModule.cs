using Autofac;
using Jack.DataScience.Storage.AWSS3;
using Jack.DataScience.Storage.AzureBlobStorage;
using Jack.DataScience.Storage.SFTP;
using System;
using System.Collections.Generic;
using System.Text;

namespace VsExamples.Athena
{
    public class VsExamplesAthenaModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataGenerator>();
            builder.RegisterModule<SshModule>();
            builder.RegisterModule<AWSS3Module>();
            builder.RegisterModule<AzureBlobStorageModule>();
        }
    }
}
