namespace Linn.Production.Facade.Tests.LabelTypesServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenAddingLabelType : ContextBase
    {
        private LabelTypeResource resource;

        private IResult<LabelType> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new LabelTypeResource
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

            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldAddLabelType()
        {
            this.LabelTypeRepository.Received().Add(Arg.Any<LabelType>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<LabelType>>();
            var dataResult = ((CreatedResult<LabelType>)this.result).Data;
            dataResult.LabelTypeCode.Should().Be(this.resource.LabelTypeCode);
            dataResult.Description.Should().Be(this.resource.Description);
            dataResult.BarcodePrefix.Should().Be(this.resource.BarcodePrefix);
            dataResult.CommandFilename.Should().Be(this.resource.CommandFilename);
            dataResult.DefaultPrinter.Should().Be(this.resource.DefaultPrinter);
            dataResult.Filename.Should().Be(this.resource.Filename);
            dataResult.TestCommandFilename.Should().Be(this.resource.TestCommandFilename);
            dataResult.TestFilename.Should().Be(this.resource.TestFilename);
            dataResult.TestPrinter.Should().Be(this.resource.TestPrinter);
            dataResult.NSBarcodePrefix.Should().Be(this.resource.NSBarcodePrefix);
        }
    }
}
