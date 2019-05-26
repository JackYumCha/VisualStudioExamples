using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Autofac;
using Jack.DataScience.Common;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace VsExamples.Athena.EtlFunction
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(string input, ILambdaContext context)
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var sftpEtl = services.Resolve<SftpEtl>();

            await sftpEtl.TransferDailySalesData();

            Debugger.Break();

            return input?.ToUpper();
        }
    }
}
