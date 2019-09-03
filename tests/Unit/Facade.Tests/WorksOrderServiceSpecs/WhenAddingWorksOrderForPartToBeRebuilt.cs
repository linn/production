﻿namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingWorksOrderForPartToBeRebuilt : ContextBase
    {
        private WorksOrderResource resource;

        private IResult<WorksOrder> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new WorksOrderResource
                                {
                                    OrderNumber = 12345,
                                    DateRaised = DateTime.Now.ToString("o"),
                                    RaisedBy = 333,
                                    PartNumber = "TROIKA/R",
                                    QuantityBuilt = 0,
                                    QuantityOutstanding = 12
                                };

            var part = new Part { BomType = "A" };

            this.PartsService.GetById(this.resource.PartNumber).Returns(new SuccessResult<Part>(part));

            this.result = this.Sut.AddWorksOrder(this.resource);
        }

        [Test]
        public void ShouldCallPartsService()
        {
            this.PartsService.Received().GetById(this.resource.PartNumber);
        }

        [Test]
        public void ShouldReturnBadRequestResult()
        {
            this.result.Should().BeOfType<BadRequestResult<WorksOrder>>();
        }
    }
}