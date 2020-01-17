namespace Linn.Production.Facade.Tests.AteTestServiceSpecs
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private AteTest testToBeUpdated;

        private AteTestResource updateResource;

        private IResult<AteTest> result;

        [SetUp]
        public void SetUp()
        {
            this.testToBeUpdated = new AteTest
                                       {
                                           TestId = 1,
                                           User = new Employee { Id = 1, FullName = "Name" },
                                           PcbOperator = new Employee { Id = 1, FullName = "Name" },
                                           NumberOfPcbFails = 1,
                                           DateTested = DateTime.UnixEpoch,
                                           WorksOrder = new WorksOrder { OrderNumber = 1, Part = new Part { PartNumber = "PART" } },
                                           Details = new List<AteTestDetail>
                                                         {
                                                             new AteTestDetail { ItemNumber = 1, TestId = 1, NumberOfFails = 1 }
                                                         }
                                       };
            this.updateResource = new AteTestResource
                                       {
                                           TestId = 1,
                                           PcbOperator = 1,
                                           PcbOperatorName = "Name",
                                           DateTested = DateTime.UnixEpoch.ToString("o"),
                                           WorksOrderNumber = 1,
                                           NumberOfPcbFails = 2,
                                           Details = new List<AteTestDetailResource>
                                                         {
                                                             new AteTestDetailResource { ItemNumber = 1, TestId = 1, NumberOfFails = 2 },
                                                             new AteTestDetailResource { ItemNumber = 2, TestId = 1, }
                                                         }
                                       };
            this.AteTestRepository.FindById(1).Returns(this.testToBeUpdated);
            this.result = this.Sut.Update(1, this.updateResource);
        }

        [Test]
        public void ShouldGetManufacturingRoute()
        {
            this.AteTestRepository.Received().FindById(1);
        }

        [Test]
        public void ShouldReturnUpdated()
        {
            var dataResult = ((SuccessResult<AteTest>)this.result).Data;
            dataResult.NumberOfPcbFails.Should().Be(2);
        }

        [Test]
        public void ShouldAddNewDetail()
        {
            this.DetailService.Received().Add(Arg.Any<AteTestDetailResource>());
        }

        [Test]
        public void ShouldUpdateExistingDetail()
        {
            this.DetailService.Received().Update(Arg.Any<AteTestDetailKey>(), Arg.Any<AteTestDetailResource>());
        }
    }
}