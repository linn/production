namespace Linn.Production.Service.Tests.LabelModuleSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingAllLabelsFails : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.LabelService.When(p => p.PrintAllLabels(808, "thing"))
                .Do(t => throw new DomainException("Fail"));

            this.Response = this.Browser.Post(
                "/production/maintenance/labels/reprint-all/808",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("articleNumber", "thing");
                }).Result;
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void ShouldCallService()
        {
            this.LabelService.Received().PrintAllLabels(808, "thing");
        }

        [Test]
        public void ShouldReturnErrorMessage()
        {
            var resource = this.Response.Body.DeserializeJson<ErrorResource>();
            resource.Errors.First().Should().Be("Fail");
        }
    }
}
