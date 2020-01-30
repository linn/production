namespace Linn.Production.Domain.Tests.LabelPrintingServiceSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenPrintingLargeBigTextLabels : ContextBase
    {
        private LabelPrintResponse result;

        [SetUp]
        public void SetUp()
        {
            var labelPrint = new LabelPrint
            {
                LabelType = (int)GeneralPurposeLabelTypes.Labels.LargeBigText,
                Printer = (int)LabelPrinters.Printers.ProdLbl2,
                Quantity = 5,
                LinesForPrinting = new LabelPrintContents { Line1 = "big yin" }
            };

            this.result = this.Sut.PrintLabel(labelPrint);
        }

        [Test]
        public void ShouldCallLabelPrintServiceForSmallLabel()
        {
            this.LabelService.Received().PrintLabel(Arg.Any<string>(), "ProdLbl2", 5, "c:\\lbl\\genLargeLabel_1line.btw", "big yin");
        }

        [Test]
        public void ShouldReceiveCorrectResult()
        {
            this.result.Message.Should().Be("printed large (big text) labels");
        }
    }
}
