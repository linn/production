namespace Linn.Production.Messaging.Dispatchers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Production.Domain.LinnApps.Dispatchers;
    using Linn.Production.Resources.Messages;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class TriggerRunDispatcher : ITriggerRunDispatcher
    {
        private const string ContentType = "application/json";

        private readonly string routingKey = "production.start-trigger-run";

        private readonly IMessageDispatcher messageDispatcher;

        public TriggerRunDispatcher(IMessageDispatcher messageDispatcher)
        {
            this.messageDispatcher = messageDispatcher;
        }

        public void StartTriggerRun(string employeeUri)
        {
            var resource = new StartTriggerRunResource { RequestedByUri = employeeUri };

            var json = JsonConvert.SerializeObject(
                resource,
                new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

            var body = Encoding.UTF8.GetBytes(json);

            this.messageDispatcher.Dispatch(this.routingKey, body, ContentType);
        }
    }
}