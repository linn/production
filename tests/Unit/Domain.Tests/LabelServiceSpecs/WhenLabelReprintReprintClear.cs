﻿namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenLabelReprintReprintClear : ContextBase
    {
        private int noOfSerialNumbers;
        private int noOfBoxes;

        private LabelReprint result;

        [SetUp]
        public void SetUp()
        {
            this.SalesArticleService.GetSmallLabelType("PART 1").Returns("CLEAR");
            this.SernosPack.When(a => a.GetSerialNumberBoxes("PART 1", out this.noOfSerialNumbers, out this.noOfBoxes))
                .Do(a =>
                    {
                        a[1] = 1;
                        a[2] = 1;
                    });
            this.LabelPack.GetLabelData("PRODUCT", 808808, "PART 1").Returns("data to be printed");
            this.LabelTypeRepository.FindById("PRODUCT")
                .Returns(new LabelType { DefaultPrinter = "printer 1", Filename = "file 1" });
            this.SernosPack.SerialNumberExists(808808, "PART 1").Returns(true);
            this.SernosPack.SerialNumbersRequired("PART 1").Returns(true);

            this.result = this.Sut.CreateLabelReprint(
                101202,
                "A good reason",
                "part 1",
                808808,
                45,
                "PRODUCT",
                1,
                "REPRINT",
                null);
        }

        [Test]
        public void ShouldNotReIssueSerialNumber()
        {
            this.SernosPack.DidNotReceive().ReIssueSernos(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>());
        }

        [Test]
        public void ShouldCheckSerialNumberExists()
        {
            this.SernosPack.Received().SerialNumberExists(808808, "PART 1");
        }

        [Test]
        public void ShouldNotRebuildSerialNumber()
        {
            this.SerialNumberRepository.DidNotReceive().Add(Arg.Any<SerialNumber>());
        }

        [Test]
        public void ShouldGetSerialNumberDetails()
        {
            this.SernosPack.Received().GetSerialNumberBoxes("PART 1", out this.noOfSerialNumbers, out this.noOfBoxes);
        }

        [Test]
        public void ShouldCallLabelPrintService()
        {
            var value = Arg.Any<string>();
            this.BartenderLabelPack.Received().PrintLabels(
                "LabelReprint808808",
                "KlimaxClear",
                1,
                "c:\\lbl\\prodLblClearDouble.btw",
                "data to be printed",
                ref value);
        }

        [Test]
        public void ShouldReturnLabelReprint()
        {
            this.result.PartNumber.Should().Be("PART 1");
            this.result.NewPartNumber.Should().BeNull();
            this.result.LabelTypeCode.Should().Be("PRODUCT");
            this.result.SerialNumber.Should().Be(808808);
            this.result.ReprintType.Should().Be("REPRINT");
            this.result.RequestedBy.Should().Be(101202);
            this.result.DocumentType.Should().Be("WO");
            this.result.WorksOrderNumber.Should().Be(45);
            this.result.DateIssued.Should().BeCloseTo(DateTime.UtcNow, 10000);
            this.result.NumberOfProducts.Should().Be(1);
        }
    }
}
