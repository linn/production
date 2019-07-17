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

    public class WhenGettingAteFaultCode : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var ateFaultCode = new AteFaultCode("FAULT")
                                   {
                                       Description = "Desc"
                                   };
            this.AteFaultCodeService.GetById("FAULT")
                .Returns(new SuccessResult<AteFaultCode>(ateFaultCode));

            this.Response = this.Browser.Get(
                "/production/quality/ate/fault-codes/FAULT",
                with =>
                    {
                        with.Header("Accept", "application/json");
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
            this.AteFaultCodeService.Received().GetById("FAULT");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<AteFaultCodeResource>();
            resource.FaultCode.Should().Be("FAULT");
            resource.Description.Should().Be("Desc");
        }
    }
}