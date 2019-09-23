namespace Linn.Production.IoC
{
    using Autofac;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Common.Messaging.RabbitMQ.Autofac;
    using Linn.Common.Messaging.RabbitMQ.Configuration;
    using Linn.Production.Domain.LinnApps.Dispatchers;
    using Linn.Production.Messaging.Dispatchers;

    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterConnectionBuilder();
            builder.RegisterInfiniteRetryStrategy();
            builder.RegisterConnector();
            builder.RegisterMessageDispatcher();
            builder.RegisterReceiver("production.q", "production.dlx");
            builder.RegisterSender("production.x", "Production Message Queuer");

            builder.RegisterType<TriggerRunDispatcher>().As<ITriggerRunDispatcher>();

            builder.RegisterType<RabbitConfiguration>().As<IRabbitConfiguration>();
            builder.RegisterType<RabbitTerminator>().As<IRabbitTerminator>();
        }
    }
}