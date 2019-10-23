namespace Linn.Production.Service.Tests.LabelModuleSpecs
{
    using FluentAssertions;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingMACLabel : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Response = this.Browser.Post(
                "/production/maintenance/labels/reprint-mac-label",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("serialNumber", "808");
                }).Result;
        }

        [Test]
        public void ShouldReturnOK()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.LabelService.Received().PrintMACLabel(808);
        }
    }
}
