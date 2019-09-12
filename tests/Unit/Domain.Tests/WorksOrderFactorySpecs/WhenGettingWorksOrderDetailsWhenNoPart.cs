namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWorksOrderDetailsWhenNoPart : ContextBase
    {
        private Action action;

        private string partNumber;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "PCAS 123";

            this.PartsRepository.FindById(this.partNumber).Returns((Part)null);

            this.action = () => this.Sut.GetWorksOrderDetails(this.partNumber);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<DomainException>().WithMessage($"No part found for part number {this.partNumber}");
        }
    }
}