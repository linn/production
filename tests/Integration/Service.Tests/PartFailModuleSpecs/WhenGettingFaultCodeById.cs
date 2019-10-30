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

    public class WhenGettingFaultCodeById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new PartFailFaultCode
                        {
                            FaultCode = "CODE"
                        };

            this.FaultCodeService.GetById("CODE").Returns(new SuccessResult<PartFailFaultCode>(a));

            this.Response = this.Browser.Get(
                "/production/quality/part-fail-fault-codes/CODE",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.FaultCodeService.Received().GetById("CODE");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartFailFaultCodeResource>();
            resource.FaultCode.Should().Be("CODE");
        }
    }
}