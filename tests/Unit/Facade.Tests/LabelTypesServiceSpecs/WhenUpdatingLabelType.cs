namespace Linn.Production.Facade.Tests.LabelTypesServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingLabelType : ContextBase
    {
        private const string NewDesc = "new description";

        private const int NewHourlyRate = 118;

        private LabelTypeResource resource;

        private IResult<LabelType> result;

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

            this.resource = new LabelTypeResource()
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

            this.LabelTypeRepository.FindById(this.labelType.LabelTypeCode)
                .Returns(this.labelType);
            this.result = this.Sut.Update(this.labelType.LabelTypeCode, this.resource);
        }

        [Test]
        public void ShouldGetLabelType()
        {
            this.LabelTypeRepository.Received().FindById(this.labelType.LabelTypeCode);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<LabelType>>();
            var dataResult = ((SuccessResult<LabelType>)this.result).Data;

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
