namespace Linn.Production.Domain.Tests.LabelPrintingServiceSpecs
{
    using System.Linq;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Products;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingPCNumberLabels : ContextBase
    {
        private LabelPrintResponse result;
        [SetUp]
        public void SetUp()
        {
            //this.LabelService.PrintLabel(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>());
            //    .Returns();
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
            this.result.Message = "printed pc numbers 1237 to 1237";
        }
    }
}
