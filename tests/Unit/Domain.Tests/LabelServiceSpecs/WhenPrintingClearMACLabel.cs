namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Products;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingClearMacLabel : ContextBase
    {
        private SerialNumber serialNumber;

        [SetUp]
        public void SetUp()
        {
            this.serialNumber = new SerialNumber("pg", "ISSUED", "AN");
            this.SerialNumberRepository.FilterBy(Arg.Any<Expression<Func<SerialNumber, bool>>>())
                .Returns(new List<SerialNumber> { this.serialNumber }.AsQueryable());
            this.SalesArticleService.GetSmallLabelType(this.serialNumber.ArticleNumber).Returns("CLEAR");
            this.ProductDataRepository.FindById(808).Returns(new ProductData { MACAddress = "99:99", ProductId = 808 });
            this.Sut.PrintMACLabel(808);
        }

        [Test]
        public void ShouldGetProductDetails()
        {
            this.ProductDataRepository.Received().FindById(808);
        }

        [Test]
        public void ShouldGetLabelDetails()
        {
            this.SalesArticleService.Received().GetSmallLabelType(this.serialNumber.ArticleNumber);
        }

        [Test]
        public void ShouldCallLabelPrintService()
        {
            var value = Arg.Any<string>();
            this.BartenderLabelPack.Received().PrintLabels(
                $"MACReprintm{808}",
                "KlimaxClear",
                2,
                "c:\\lbl\\macAddressClearDouble.btw",
                "99:99",
                ref value);
        }
    }
}
