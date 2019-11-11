﻿namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByPartAndTooManyResults : ContextBase
    {
        private IResult<IEnumerable<WorksOrder>> result;

        private string part;

        [SetUp]
        public void SetUp()
        {
            var results = new List<WorksOrder>();

            for (int i = 0; i <= 2000; i++)
            {
                results.Add(new WorksOrder { OrderNumber = i, Part = new Part { PartNumber = "part" } });
            }

            this.part = "part";
            this.WorksOrderRepository.FilterBy(Arg.Any<Expression<Func<WorksOrder, bool>>>())
                .Returns(results.AsQueryable());
            this.result = this.Sut.SearchByBoardNumber(this.part);
        }

        [Test]
        public void ShouldGetWorksOrders()
        {
            this.WorksOrderRepository.Received().FilterBy(Arg.Any<Expression<Func<WorksOrder, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<WorksOrder>>>();
            var dataResult = ((SuccessResult<IEnumerable<WorksOrder>>)this.result).Data;
            dataResult.Count().Should().Be(1000);
        }

        [Test]
        public void ShouldLimitResults()
        {
            var dataResult = ((SuccessResult<IEnumerable<WorksOrder>>)this.result).Data;
            dataResult.Count().Should().Be(1000);
        }
    }
}