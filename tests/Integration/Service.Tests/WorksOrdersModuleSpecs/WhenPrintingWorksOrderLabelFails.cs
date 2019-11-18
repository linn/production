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

    public class WhenPrintingWorksOrderLabelFails : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.WorksOrderLabelPack.When(p => p.PrintLabels(123, "printer"))
                .Do(t => throw new DomainException("Fail"));

            this.Response = this.Browser.Post(
                "/production/works-orders/print-labels",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("orderNumber", "123");
                        with.Query("printerGroup", "printer");
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
            this.WorksOrderLabelPack.Received().PrintLabels(123, "printer");
        }

        [Test]
        public void ShouldReturnErrorMessage()
        {
            var resource = this.Response.Body.DeserializeJson<ErrorResource>();
            resource.Errors.First().Should().Be("Fail");
        }
    }
}