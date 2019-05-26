using Autofac;
using Jack.DataScience.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace VsExamples.Athena.Tests
{
    public class TransferData
    {
        [Fact(DisplayName = "Trasfer Sftp Products data to S3")]
        public async void TransferProductsData()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var sftpEtl = services.Resolve<SftpEtl>();

            await sftpEtl.TransferProductsData();

            Debugger.Break();
        }

        [Fact(DisplayName = "Trasfer Sftp Daily Sales data to S3")]
        public async void TransferDailySalesData()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var sftpEtl = services.Resolve<SftpEtl>();

            await sftpEtl.TransferDailySalesData();

            Debugger.Break();
        }
    }
}
