namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using FluentAssertions;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingWorksOrderLabel : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
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
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.WorksOrderLabelPack.Received().PrintLabels(123, "printer");
        }
    }
}
