namespace Linn.Production.Service.Tests.LabelModuleSpecs
{
    using System.Linq;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenReprintingSerialNumber : ContextBase
    {
        private LabelReprintResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new LabelReprintResource { LabelReprintId = 2 };

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

            this.LabelReprintFacadeService.Add(Arg.Any<LabelReprintResource>())
                .Returns(new CreatedResult<LabelReprint>(labelReprint));

            this.Response = this.Browser.Post(
                "/production/maintenance/labels/reprint-serial-number",
                with =>
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
        public void ShouldCallService()
        {
            this.LabelReprintFacadeService
                .Received()
                .Add(Arg.Any<LabelReprintResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<LabelReprintResource>();
            resource.LabelReprintId.Should().Be(2);
            resource.LabelTypeCode.Should().Be("BOX");
            resource.DocumentType.Should().Be("W");
            resource.NewPartNumber.Should().Be("N");
            resource.PartNumber.Should().Be("P");
            resource.WorksOrderNumber.Should().Be(3);
            resource.Reason.Should().Be("Good");
            resource.NumberOfProducts.Should().Be(2);
            resource.SerialNumber.Should().Be(3);
            resource.Links.First(a => a.Rel == "requested-by").Href.Should().Be("/employees/3423");
            resource.DateIssued.Should().Be(1.February(2021).ToString("o"));
            resource.ReprintType.Should().Be("RSN REISSUE");
        }
    }
}
