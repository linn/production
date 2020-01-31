namespace Linn.Production.Domain.Tests.LabelPrintingServiceSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenPrintingPCNumberLabels : ContextBase
    {
        private LabelPrintResponse result;

        [SetUp]
        public void SetUp()
        {
            var labelPrint = new LabelPrint
            {
                LabelType = (int)GeneralPurposeLabelTypes.Labels.PCNumbers,
                Printer = (int)LabelPrinters.Printers.ProdLbl1,
                Quantity = 4,
                LinesForPrinting = new LabelPrintContents { FromPCNumber = "1237" }
            };

            this.result = this.Sut.PrintLabel(labelPrint);
        }

        [Test]
        public void ShouldCallLabelPrintServiceForPCNumber()
        {
            this.LabelService.Received().PrintLabel(Arg.Any<string>(), "ProdLbl1", 4, "c:\\lbl\\PCLabel.btw", "\"PC1237\"");
        }

        [Test]
        public void ShouldReceiveCorrectResult()
        {
            this.result.Message.Should().Be("printed pc numbers 1237 to 1237");
        }
    }
}
