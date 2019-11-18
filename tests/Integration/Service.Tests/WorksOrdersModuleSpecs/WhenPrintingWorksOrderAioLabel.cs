namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using FluentAssertions;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingWorksOrderAioLabel : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
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
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.WorksOrderLabelPack.Received().PrintAioLabels(123);
        }
    }
}