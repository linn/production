namespace Linn.Production.Messaging.Handlers
{
    using System;
    using System.Text;

    using Linn.Common.Logging;
    using Linn.Common.Messaging.RabbitMQ.Unicast;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Resources.Messages;

    using Newtonsoft.Json;

    public class StartTriggerRunHandler
    {
        private readonly ILog log;

        private readonly ITriggerRunPack triggerRunPack;

        public StartTriggerRunHandler(ILog log, ITriggerRunPack triggerRunPack)
        {
            this.log = log;
            this.triggerRunPack = triggerRunPack;
        }

        public bool Execute(IReceivedMessage message)
        {
            var content = Encoding.UTF8.GetString(message.Body);
            var resource = JsonConvert.DeserializeObject<StartTriggerRunResource>(content);
            this.log.Warning($"Trigger run started at {DateTime.Now.ToLongTimeString()} by {resource.RequestedByUri}");
            this.triggerRunPack.AutoTriggerRun();
            return true;
        }
    }
}