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

    public class WhenGettingLabelType : ContextBase
    {
        private LabelType labelType;

        [SetUp]
        public void SetUp()
        {
            this.labelType = new LabelType
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

            this.LabelTypeService.GetById("newTcode")
                .Returns(new SuccessResult<LabelType>(labelType));

            this.Response = this.Browser.Get(
                "/production/resources/label-types/newTcode",
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
            this.LabelTypeService.Received().GetById("newTcode");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<LabelTypeResource>();
            resource.LabelTypeCode.Should().Be(this.labelType.LabelTypeCode);
            resource.Description.Should().Be(this.labelType.Description);
            resource.BarcodePrefix.Should().Be(this.labelType.BarcodePrefix);
            resource.CommandFilename.Should().Be(this.labelType.CommandFilename);
            resource.DefaultPrinter.Should().Be(this.labelType.DefaultPrinter);
            resource.Filename.Should().Be(this.labelType.Filename);
            resource.TestCommandFilename.Should().Be(this.labelType.TestCommandFilename);
            resource.TestFilename.Should().Be(this.labelType.TestFilename);
            resource.TestPrinter.Should().Be(this.labelType.TestPrinter);
            resource.NSBarcodePrefix.Should().Be(this.labelType.NSBarcodePrefix);
        }
    }
}
