namespace Linn.Production.Messaging.Tests.Handlers.StartTriggerRunHandlerSpecs
{
    using System.Text;

    using FluentAssertions;

    using Linn.Common.Messaging.RabbitMQ.Unicast;
    using Linn.Production.Resources.Messages;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingTriggerRun : ContextBase
    {
        private bool result;

        [SetUp]
        public void SetUp()
        {
            var message = Substitute.For<IReceivedMessage>();
            var json = JsonConvert.SerializeObject(
                new StartTriggerRunResource { RequestedByUri = "/e/40" },
                new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

            var body = Encoding.UTF8.GetBytes(json);
            message.Body.Returns(body);
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