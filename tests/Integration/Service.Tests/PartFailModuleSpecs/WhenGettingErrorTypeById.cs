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

    public class WhenGettingErrorTypeById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new PartFailErrorType
                        {
                            ErrorType = "ERROR"
                        };

            this.ErrorTypeService.GetById("ERROR").Returns(new SuccessResult<PartFailErrorType>(a));

            this.Response = this.Browser.Get(
                "/production/quality/part-fail-error-types/ERROR",
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
            this.ErrorTypeService.Received().GetById("ERROR");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartFailResource>();
            resource.ErrorType.Should().Be("ERROR");
        }
    }
}