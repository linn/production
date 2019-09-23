namespace Linn.Production.Messaging.Tests.Dispatchers.TriggerRunDispatcherSpecs
{
    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Production.Domain.LinnApps.Dispatchers;
    using Linn.Production.Messaging.Dispatchers;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ITriggerRunDispatcher Sut { get; private set; }

        protected IMessageDispatcher MessageDispatcher { get; private set; }


        [SetUp]
        public void EstablishContext()
        {
            this.MessageDispatcher = Substitute.For<IMessageDispatcher>();
            this.Sut = new TriggerRunDispatcher(this.MessageDispatcher);
        }
    }
}