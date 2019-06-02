using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VsExamples.Lambda.SQS.Data;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace VsExamples.Lambda.SQS
{
    public class Function
    {
        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
        /// to respond to SQS messages.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            foreach(var message in evnt.Records)
            {
                await ProcessMessageAsync(message, context);
            }
        }

        private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
        {
            context.Logger.LogLine($"Start Processed message {message.Body}");

            var json = message.Body;
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
            {
                Converters = { new StringEnumConverter() }
            };

            CustomizedMessage customizedMessage = JsonConvert.DeserializeObject<CustomizedMessage>(json, jsonSerializerSettings);


            Thread.Sleep(5000);
            context.Logger.LogLine($"CustomizedMessage: Type={customizedMessage.Type} Name={customizedMessage.CustomerName} Value={customizedMessage.Value}");
            // TODO: Do interesting work based on the new message
            await Task.CompletedTask;
        }
    }
}
