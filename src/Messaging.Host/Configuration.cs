namespace Linn.Production.Messaging.Host
{
    using Autofac;

    using Linn.Common.Messaging.RabbitMQ.Autofac;
    using Linn.Production.IoC;

    public static class Configuration
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AmazonCredentialsModule>();
            builder.RegisterModule<AmazonSqsModule>();
            builder.RegisterModule<LoggingModule>();
            builder.RegisterModule<MessagingModule>();
            builder.RegisterReceiver("production.q", "production.dlx");

            builder.RegisterType<Listener>().AsSelf();

            return builder.Build();
        }
    }
}
