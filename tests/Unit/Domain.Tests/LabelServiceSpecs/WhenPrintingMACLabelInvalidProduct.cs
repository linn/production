namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Production.Domain.LinnApps.Products;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingMACLabelInvalidProduct : ContextBase
    {
        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.ProductDataRepository.FindById(808).Returns((ProductData)null);
            this.action = () => this.Sut.PrintMACLabel(808);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<DomainException>();
        }
    }
}
