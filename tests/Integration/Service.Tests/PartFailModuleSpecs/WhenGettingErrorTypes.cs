namespace Linn.Production.Service.Tests.PartFailModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingErrorTypes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new PartFailErrorType { ErrorType = "Error A" };
            var b = new PartFailErrorType { ErrorType = "Error B" };

            var errorTypes = new List<PartFailErrorType> { a, b };
            this.ErrorTypeService.GetAll().Returns(new SuccessResult<IEnumerable<PartFailErrorType>>(errorTypes));

            this.Response = this.Browser.Get(
                "/production/quality/part-fail-error-types",
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
            this.ErrorTypeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartFailErrorTypeResource>>();
            resource.Count().Should().Be(2);
        }
    }
}