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

    public class WhenUpdatingFaultCode : ContextBase
    {
        private PartFailFaultCodeResource requestResource;

        [SetUp]
        public void SetUp()
        {
            var a = new PartFailFaultCode
            {
                FaultCode = "CODE"
            };

            this.requestResource = new PartFailFaultCodeResource { FaultCode = "CODE" };

            this.FaultCodeService.Update("CODE", Arg.Any<PartFailFaultCodeResource>()).Returns(new SuccessResult<PartFailFaultCode>(a));

            this.Response = this.Browser.Put(
                "/production/quality/part-fail-fault-codes/CODE",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.JsonBody(this.requestResource);
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.FaultCodeService
                .Received()
                .Update("CODE", Arg.Is<PartFailFaultCodeResource>(r => r.FaultCode == this.requestResource.FaultCode));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartFailFaultCodeResource>();
            resource.FaultCode.Should().Be("CODE");
        }
    }
}