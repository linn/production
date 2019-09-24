namespace Linn.Production.Messaging.Tests.Handlers.StartTriggerRunHandlerSpecs
{
    using FluentAssertions;

    using Linn.Common.Messaging.RabbitMQ.Unicast;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingTriggerRun : ContextBase
    {
        private bool result;

        [SetUp]
        public void SetUp()
        {
            var message = Substitute.For<IReceivedMessage>();
            this.result = this.Sut.Execute(message);
        }

        [Test]
        public void ShouldStartTriggerRun()
        {
            this.TriggerRunPack.Received().AutoTriggerRun();
        }

        [Test]
        public void ShouldReturnTrue()
        {
            this.result.Should().BeTrue();
        }
    }
}