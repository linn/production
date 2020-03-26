namespace Linn.Production.Service.Tests.PartCadInfoModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        private string searchTerm;

        [SetUp]
        public void SetUp()
        {
            var a = new PartCadInfo { MsId = 1, PartNumber = "CONN 1" };
            var b = new PartCadInfo { MsId = 2, PartNumber = "CONN 2" };

            this.searchTerm = "CONN";

            this.PartCadInfoService.Search("CONN", Arg.Any<IEnumerable<string>>()).Returns(
                new SuccessResult<ResponseModel<IEnumerable<PartCadInfo>>>(
                    new ResponseModel<IEnumerable<PartCadInfo>>(new List<PartCadInfo> { a, b }, new List<string>())));

            this.Response = this.Browser.Get(
                "/production/maintenance/part-cad-info",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", this.searchTerm);
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
            this.PartCadInfoService.Received().Search(this.searchTerm, Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<PartCadInfoResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(r => r.PartNumber == "CONN 1");
            resources.Should().Contain(r => r.PartNumber == "CONN 2");
        }
    }
}