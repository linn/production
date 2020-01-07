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

    public class WhenReprintingReissueSerialNumberWithoutPermission : ContextBase
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

            this.LabelReprintFacadeService.Add(Arg.Any<LabelReprintResource>(), Arg.Any<IEnumerable<string>>())
                .Returns(new CreatedResult<ResponseModel<LabelReprint>>(new ResponseModel<LabelReprint>(labelReprint, new List<string>())));
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.SerialNumberReissueRebuild, Arg.Any<IEnumerable<string>>())
                .Returns(false);
            this.Response = this.Browser.Post(
                "/production/maintenance/labels/reprint-reasons",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnUnauthorised()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
