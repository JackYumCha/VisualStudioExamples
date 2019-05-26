using Autofac;
using Jack.DataScience.Common;
using System;
using System.Diagnostics;

namespace VsExamples.Athena.EtlBatch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AWS Batch Job Start!");

            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var sftpEtl = services.Resolve<SftpEtl>();

            sftpEtl.TransferDailySalesData().GetAwaiter().GetResult();

            Debugger.Break();

            Console.WriteLine("AWS Batch Job Completed!");
        }
    }
}
