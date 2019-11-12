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

    public class WhenAddingLabelType : ContextBase
    {
        private LabelTypeResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new LabelTypeResource
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
            var newLabelType = new LabelType
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

            this.LabelTypeService.Add(Arg.Any<LabelTypeResource>())
                .Returns(new CreatedResult<LabelType>(newLabelType));

            this.Response = this.Browser.Post(
                "/production/resources/label-types",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.LabelTypeService.Received()
                .Add(Arg.Is<LabelTypeResource>(r => r.LabelTypeCode == this.requestResource.LabelTypeCode));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<LabelTypeResource>();
            resource.LabelTypeCode.Should().Be(this.requestResource.LabelTypeCode);
            resource.Description.Should().Be(this.requestResource.Description);
            resource.BarcodePrefix.Should().Be(this.requestResource.BarcodePrefix);
            resource.CommandFilename.Should().Be(this.requestResource.CommandFilename);
            resource.DefaultPrinter.Should().Be(this.requestResource.DefaultPrinter);
            resource.Filename.Should().Be(this.requestResource.Filename);
            resource.TestCommandFilename.Should().Be(this.requestResource.TestCommandFilename);
            resource.TestFilename.Should().Be(this.requestResource.TestFilename);
            resource.TestPrinter.Should().Be(this.requestResource.TestPrinter);
            resource.NSBarcodePrefix.Should().Be(this.requestResource.NSBarcodePrefix);
        }
    }
}
