namespace Linn.Production.Messaging.Handlers
{
    using System;
    using System.Text;

    using Linn.Common.Logging;
    using Linn.Common.Messaging.RabbitMQ.Unicast;

    public class StartTriggerRunHandler
    {
        private readonly ILog log;

        public StartTriggerRunHandler(ILog log)
        {
            this.log = log;
        }

        public bool Execute(IReceivedMessage message)
        {
            var content = Encoding.UTF8.GetString(message.Body);
            this.log.Info($"Trigger run started at {DateTime.Now.ToLongTimeString()}");
            return true;
        }
    }
}