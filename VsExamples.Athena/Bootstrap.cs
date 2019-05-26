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

namespace VsExamples.Athena
{
    public static class Bootstrap
    {
        public static AutoFacContainer Setup(this AutoFacContainer autoFacContainer)
        {
            autoFacContainer.RegisterOptions<SshOptions>();
            autoFacContainer.RegisterOptions<AWSS3Options>();
            autoFacContainer.RegisterOptions<AzureBlobStorageOptions>();
            autoFacContainer.RegisterOptions<AWSAthenaOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<VsExamplesAthenaModule>();
            return autoFacContainer;
        }
    }
}
