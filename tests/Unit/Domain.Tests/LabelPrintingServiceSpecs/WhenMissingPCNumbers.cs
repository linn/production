namespace Linn.Production.Domain.Tests.LabelPrintingServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMissingPCNumbers : ContextBase
    {
        private Action action;

        [SetUp]
        public void SetUp()
        {
            var labelPrint = new LabelPrint
                                 {
                                     LabelType = (int)GeneralPurposeLabelTypes.Labels.PCNumbers,
                                     Printer = (int)LabelPrinters.Printers.ProdLbl1,
                                     Quantity = 4,
                                     LinesForPrinting = new LabelPrintContents { FromPCNumber = null }
                                 };

            this.action = () => this.Sut.PrintLabel(labelPrint);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<ArgumentException>("No PC number provided");
        }
    }
}
