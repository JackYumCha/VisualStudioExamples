using Autofac;
using Jack.DataScience.Common;
using Jack.DataScience.MQ.AWSSQS;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using VsExamples.Lambda.SQS.Data;
using Xunit;

namespace VsExamples.Lambda.SQSTests
{
    public class SQSSendTests
    {
        [Fact(DisplayName = "Send Message to SQS")]
        public async void SendMessageToSQS()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.RegisterOptions<AWSSQSOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<AWSSQSModule>();

            var services = autoFacContainer.ContainerBuilder.Build();

            var awsSQSAPI = services.Resolve<AWSSQSAPI>();

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
            {
                Converters = { new StringEnumConverter() }
            };


            for(int i = 0; i < 100; i++)
            {
                await awsSQSAPI.SendMessage(JsonConvert.SerializeObject(new CustomizedMessage()
                {
                    Type = MessageTypeEnum.Promotional,
                    CustomerName = $"John Smith  #{i}",
                    Value = "hello!"
                }, jsonSerializerSettings));
            }

        }
    }
}
