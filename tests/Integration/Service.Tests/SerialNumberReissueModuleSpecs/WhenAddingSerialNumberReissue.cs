namespace Linn.Production.Service.Tests.SerialNumberReissueModuleSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Domain.LinnApps.SerialNumberReissue;
    using Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenAddingSerialNumberReissue : ContextBase
    {
        private SerialNumberReissueResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new SerialNumberReissueResource { ArticleNumber = "art", SernosGroup = "group" } ;
            var serialNumberReissue = new SerialNumberReissue("group", "art");
            this.SerialNumberReissueService.Add(Arg.Any<SerialNumberReissueResource>())
                .Returns(new CreatedResult<SerialNumberReissue>(serialNumberReissue));

            this.Response = this.Browser.Post(
                "/production/maintenance/serial-number-reissue", with =>
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
            this.SerialNumberReissueService.Received()
                .Add(Arg.Is<SerialNumberReissueResource>(r => r.SernosGroup == this.requestResource.SernosGroup));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<SerialNumberReissueResource>();
            resource.SernosGroup.Should().Be("group");
        }
    }
}
