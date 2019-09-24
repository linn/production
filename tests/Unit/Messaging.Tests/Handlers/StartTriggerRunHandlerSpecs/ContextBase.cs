namespace Linn.Production.Messaging.Tests.Handlers.StartTriggerRunHandlerSpecs
{
    using Linn.Common.Logging;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Messaging.Handlers;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected StartTriggerRunHandler Sut { get; private set; }

        protected ILog Log { get; private set; }

        protected ITriggerRunPack TriggerRunPack { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.Log = Substitute.For<ILog>();
            this.TriggerRunPack = Substitute.For<ITriggerRunPack>();

            this.Sut = new StartTriggerRunHandler(this.Log, this.TriggerRunPack);
        }
    }
}