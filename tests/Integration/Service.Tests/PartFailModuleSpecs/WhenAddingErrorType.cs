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

    public class WhenAddingErrorType : ContextBase
    {
        private PartFailErrorTypeResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new PartFailErrorTypeResource
            {
                ErrorType = "ERROR"
            };

            var partFailErrorType = new PartFailErrorType
            {
                ErrorType = "ERROR"
            };

            this.ErrorTypeService.Add(Arg.Any<PartFailErrorTypeResource>())
                .Returns(new CreatedResult<PartFailErrorType>(partFailErrorType));

            this.Response = this.Browser.Post(
                "/production/quality/part-fail-error-types",
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
            this.ErrorTypeService
                .Received()
                .Add(Arg.Any<PartFailErrorTypeResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartFailErrorTypeResource>();
            resource.ErrorType.Should().Be("ERROR");
        }
    }
}