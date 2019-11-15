namespace Linn.Production.Service.Tests.LabelTypesModuleSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    public class WhenGettingLabelTypes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var firstLabelType = new LabelType
            {
                LabelTypeCode = "newTcode",
                BarcodePrefix = "pf",
                CommandFilename = "cmdfilename",
                DefaultPrinter = "printer1",
                Description = "desc",
                Filename = "filenm",
                NSBarcodePrefix = "ns",
                TestCommandFilename = "tstcmdfilenm",
                TestFilename = "testfilenm",
                TestPrinter = "testprintr"
            };


            var secondLabelType = new LabelType
            {
                LabelTypeCode = "newTcode2",
                BarcodePrefix = "pf",
                CommandFilename = "cmdfilename",
                DefaultPrinter = "printer1",
                Description = "desc2",
                Filename = "filenm",
                NSBarcodePrefix = "ns",
                TestCommandFilename = "tstcmdfilenm",
                TestFilename = "testfilenm",
                TestPrinter = "testprintr"
            };

            this.LabelTypeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<LabelType>>(new List<LabelType> { firstLabelType, secondLabelType }));

            this.Response = this.Browser.Get(
                "/production/resources/label-types",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.LabelTypeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<LabelTypeResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.LabelTypeCode == "newTcode");
            resources.Should().Contain(a => a.LabelTypeCode == "newTcode2");
        }
    }
}
