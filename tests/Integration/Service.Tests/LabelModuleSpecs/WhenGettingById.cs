namespace Linn.Production.Service.Tests.LabelModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var labelReprint = new LabelReprint
                                   {
                                       LabelReprintId = 2,
                                       LabelTypeCode = "BOX",
                                       DocumentType = "W",
                                       NewPartNumber = "N",
                                       PartNumber = "P",
                                       WorksOrderNumber = 3,
                                       Reason = "Good",
                                       NumberOfProducts = 2,
                                       SerialNumber = 3,
                                       RequestedBy = 3423,
                                       DateIssued = 1.February(2021),
                                       ReprintType = "RSN REISSUE"
                                   };

            this.LabelReprintFacadeService.GetById(2, Arg.Any<IEnumerable<string>>())
                .Returns(new SuccessResult<ResponseModel<LabelReprint>>(new ResponseModel<LabelReprint>(labelReprint, new List<string>())));

            this.Response = this.Browser.Get(
                "/production/maintenance/labels/reprint-reasons/2",
                with =>
                    {
                        with.Header("Accept", "application/json");
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
            this.LabelReprintFacadeService.Received().GetById(2, Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<LabelReprintResource>();
            resource.LabelReprintId.Should().Be(2);
        }

        [Test]
        public void ShouldContainLinks()
        {
            var resource = this.Response.Body.DeserializeJson<LabelReprintResource>();
            resource.Links.Should().HaveCount(3);
            resource.Links.Should().Contain(l => l.Rel == "self" && l.Href == "/production/maintenance/labels/reprint-reasons/2");
            resource.Links.Should().Contain(l => l.Rel == "create" && l.Href == "/production/maintenance/labels/reprint-reasons");
            resource.Links.Should().Contain(l => l.Rel == "requested-by");
        }
    }
}