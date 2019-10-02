namespace Linn.Production.Messaging.Tests.Dispatchers.TriggerRunDispatcherSpecs
{
    using NSubstitute;

    using NUnit.Framework;

    public class WhenSendingStartTriggerRun : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.StartTriggerRun("/e/1");
        }

        [Test]
        public void ShouldSendMessage()
        {
            this.MessageDispatcher.Received().Dispatch(
                "production.start-trigger-run",
                Arg.Any<byte[]>(),
                "application/json");
        }
    }
}
