using System;

namespace VsExamples.Lambda.SQS.Data
{
    public class CustomizedMessage
    {
        public MessageTypeEnum Type { get; set; }
        public string Value { get; set; }
        public string CustomerName { get; set; }
    }

    public enum MessageTypeEnum
    {
        Promotional,
        Transactional,
    }
}
