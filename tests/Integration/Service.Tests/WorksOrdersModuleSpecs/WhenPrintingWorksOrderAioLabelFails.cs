namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingWorksOrderAioLabelFails : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.WorksOrderLabelPack.When(p => p.PrintAioLabels(123))
                .Do(t => throw new DomainException("Fail"));

            this.Response = this.Browser.Post(
                "/production/works-orders/print-aio-labels",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("orderNumber", "123");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void ShouldCallService()
        {
            this.WorksOrderLabelPack.Received().PrintAioLabels(123);
        }

        [Test]
        public void ShouldReturnErrorMessage()
        {
            var resource = this.Response.Body.DeserializeJson<ErrorResource>();
            resource.Errors.First().Should().Be("Fail");
        }
    }
}