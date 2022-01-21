namespace Linn.Production.Service.Tests.PartFailModuleSpecs
{
    using System;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
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
                            ErrorType = "ERROR",
                            DateInvalid = DateTime.Parse("21/01/2021")
                        };

            this.requestResource = new PartFailErrorTypeResource { ErrorType = "ERROR", DateInvalid = 21.January(2021).ToString("O") };

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
            resource.DateInvalid.Should().Be("2021-01-21T00:00:00.0000000");
        }
    }
}