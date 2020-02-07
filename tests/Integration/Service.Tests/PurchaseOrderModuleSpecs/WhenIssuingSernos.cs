namespace Linn.Production.Service.Tests.PurchaseOrderModuleSpecs
{
    using FluentAssertions;

    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenIssuingSernos : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var resource = new IssueSernosRequestResource()
                               {
                                   DocumentLine = 1,
                                   DocumentNumber = 1,
                                   PartNumber = "PART",
                                   DocumentType = "PO"
                               };

            this.Response = this.Browser.Post(
                "/production/resources/purchase-orders/issue-sernos",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(resource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallProxy()
        {
            this.SernosPack.Received().IssueSernos(
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<int>());
        }
    }
}