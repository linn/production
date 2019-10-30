namespace Linn.Production.Service.Tests.LabelModuleSpecs
{
    using FluentAssertions;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingAllLabels : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Response = this.Browser.Post(
                "/production/maintenance/labels/reprint-all",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("articleNumber", "article");
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
            this.LabelService.Received().PrintAllLabels(808, "article");
        }
    }
}
