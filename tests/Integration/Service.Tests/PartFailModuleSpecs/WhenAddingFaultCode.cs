namespace Linn.Production.Service.Tests.PartFailModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingFaultCode : ContextBase
    {
        private PartFailFaultCodeResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new PartFailFaultCodeResource
                                       {
                                           FaultCode = "CODE"
                                       };

            var partFailFaultCode = new PartFailFaultCode
                                        {
                                            FaultCode = "CODE"
                                        };

            this.FaultCodeService.Add(Arg.Any<PartFailFaultCodeResource>())
                .Returns(new CreatedResult<PartFailFaultCode>(partFailFaultCode));

            this.Response = this.Browser.Post(
                "/production/quality/part-fail-fault-codes",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.requestResource);
                }).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.FaultCodeService
                .Received()
                .Add(Arg.Any<PartFailFaultCodeResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartFailFaultCodeResource>();
            resource.FaultCode.Should().Be("CODE");
        }
    }
}