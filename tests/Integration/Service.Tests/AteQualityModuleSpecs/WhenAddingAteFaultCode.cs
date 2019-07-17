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

    public class WhenAddingAteFaultCode : ContextBase
    {
        private AteFaultCodeResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new AteFaultCodeResource { FaultCode = "F1" };
            var ateFaultCode = new AteFaultCode("F1");
            this.AteFaultCodeService.Add(Arg.Any<AteFaultCodeResource>())
                .Returns(new CreatedResult<AteFaultCode>(ateFaultCode));

            this.Response = this.Browser.Post(
                "/production/quality/ate/fault-codes/",
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
            this.AteFaultCodeService
                .Received()
                .Add(Arg.Is<AteFaultCodeResource>(r => r.FaultCode == this.requestResource.FaultCode));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<AteFaultCodeResource>();
            resource.FaultCode.Should().Be("F1");
        }
    }
}