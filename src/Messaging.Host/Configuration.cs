namespace Linn.Production.Messaging.Host
{
    using Autofac;

    using Linn.Common.Messaging.RabbitMQ.Autofac;
    using Linn.Production.IoC;
    using Linn.Production.Messaging.Handlers;

    public static class Configuration
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AmazonCredentialsModule>();
            builder.RegisterModule<AmazonSqsModule>();
            builder.RegisterModule<LoggingModule>();
            builder.RegisterModule<PersistenceModule>();
            builder.RegisterModule<MessagingModule>();
            builder.RegisterReceiver("production.q", "production.dlx");

            builder.RegisterType<StartTriggerRunHandler>().AsSelf();
            builder.RegisterType<Listener>().AsSelf();

            return builder.Build();
        }
    }
}
