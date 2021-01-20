namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using Linn.Production.Domain.LinnApps.Products;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingAllLabelsClear : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.ProductDataRepository.FindById(808)
                .Returns(new ProductData { MACAddress = "99:99", ProductId = 808 });
            this.SalesArticleService.GetSmallLabelType("article").Returns("CLEAR");
            this.Sut.PrintAllLabels(808, "article");
        }

        [Test]
        public void ShouldGetProductDetails()
        {
            this.ProductDataRepository.Received().FindById(808);
        }

        [Test]
        public void ShouldGetLabelData()
        {
            this.LabelPack.Received().GetLabelData("BOX", 808, "article");
        }

        [Test]
        public void ShouldCallLabelPrintServiceForMacLabel()
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

        [Test]
        public void ShouldCallLabelPrintServiceForProductLabel()
        {
            var value = Arg.Any<string>();
            this.BartenderLabelPack.Received().PrintLabels(
                $"ProductReprint{808}",
                "KlimaxClear",
                3,
                "c:\\lbl\\prodLblClearDouble.btw",
                Arg.Any<string>(),
                ref value);
        }

        [Test]
        public void ShouldCallLabelPrintServiceForBoxLabel()
        {
            var value = Arg.Any<string>();
            this.BartenderLabelPack.Received().PrintLabels(
                $"BoxReprint{808}",
                "ProdLbl2",
                1,
                "c:\\lbl\\boxlbl_08ean.btw",
                Arg.Any<string>(),
                ref value);
        }
    }
}
