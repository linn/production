namespace Linn.Production.Service.Tests.AteQualityModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingAteFaultCode : ContextBase
    {
        private AteFaultCodeResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new AteFaultCodeResource { FaultCode = "F1", Description = "Desc" };
            var ateFaultCode = new AteFaultCode("F1") { Description = "Desc" };
            this.AteFaultCodeService.Update("F1", Arg.Any<AteFaultCodeResource>())
                .Returns(new SuccessResult<AteFaultCode>(ateFaultCode));

            this.Response = this.Browser.Put(
                "/production/quality/ate/fault-codes/F1",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
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
            this.AteFaultCodeService
                .Received()
                .Update("F1", Arg.Is<AteFaultCodeResource>(r => r.FaultCode == this.requestResource.FaultCode));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<AteFaultCodeResource>();
            resource.FaultCode.Should().Be("F1");
            resource.Description.Should().Be("Desc");
        }
    }
}