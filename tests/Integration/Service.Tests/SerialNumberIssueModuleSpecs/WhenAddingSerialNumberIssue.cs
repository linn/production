namespace Linn.Production.Service.Tests.SerialNumberIssueModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Domain.LinnApps.SerialNumberIssue;
    using Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingSerialNumberIssue : ContextBase
    {
        private SerialNumberIssueResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new SerialNumberIssueResource { ArticleNumber = "art", SernosGroup = "group" } ;
            var serialNumberIssue = new SerialNumberIssue("group", "art");
            this.SerialNumberIssueService.Add(Arg.Any<SerialNumberIssueResource>())
                .Returns(new CreatedResult<SerialNumberIssue>(serialNumberIssue));

            this.Response = this.Browser.Post(
                "/production/maintenance/serial-number-issue", with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.requestResource);
                }).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void SholdCallService()
        {
            this.SerialNumberIssueService.Received()
                .Add(Arg.Is<SerialNumberIssueResource>(r => r.SernosGroup == this.requestResource.SernosGroup));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<SerialNumberIssueResource>();
            resource.SernosGroup.Should().Be("group");
        }
    }
}
