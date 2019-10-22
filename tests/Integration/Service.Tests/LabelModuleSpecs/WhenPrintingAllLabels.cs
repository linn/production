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
                "/production/maintenance/labels/reprint-all/808",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("articleNumber", "article");
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
