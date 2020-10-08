namespace Linn.Production.Service.Tests.PurchaseOrderModuleSpecs
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Resources;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenIssuingSernosAndError : ContextBase
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
            this.SernosPack.When(fake => fake.IssueSernos(
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<int?>())).Do(call => throw new Exception("Fail"));


            this.Response = this.Browser.Post(
                "/production/resources/purchase-orders/issue-sernos",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(resource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
                Arg.Any<int?>());
        }

        [Test]
        public void ShouldReturnError()
        {
            var resource = this.Response.Body.DeserializeJson<ErrorResource>();
            resource.Errors.First().Should().Be("Fail");
        }
    }
}