namespace Linn.Production.Service.Tests.PartFailModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingErrorType : ContextBase
    {
        private PartFailErrorTypeResource requestResource;

        [SetUp]
        public void SetUp()
        {
            var a = new PartFailErrorType
                        {
                            ErrorType = "ERROR"
                        };

            this.requestResource = new PartFailErrorTypeResource { ErrorType = "ERROR" };

            this.ErrorTypeService.Update("ERROR", Arg.Any<PartFailErrorTypeResource>()).Returns(new SuccessResult<PartFailErrorType>(a));

            this.Response = this.Browser.Put(
                "/production/quality/part-fail-error-types/ERROR",
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
            this.ErrorTypeService
                .Received()
                .Update("ERROR", Arg.Is<PartFailErrorTypeResource>(r => r.ErrorType == this.requestResource.ErrorType));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartFailErrorTypeResource>();
            resource.ErrorType.Should().Be("ERROR");
        }
    }
}