namespace Linn.Production.Facade.Tests.AteTestDetailSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingFirstDetail : ContextBase
    {
        private AteTestDetailResource resource;

        private IResult<AteTestDetail> result;

        [SetUp]
        public void SetUp()
        {
            this.EmployeeRepository.FindById(1).Returns(new Employee { Id = 1 });

            this.AteTestRepository.FindById(1).Returns(new AteTest { TestId = 1 });

            this.EmployeeRepository.FilterBy(Arg.Any<Expression<Func<Employee, bool>>>())
                .Returns(new List<Employee>
                             {
                                 new Employee { Id = 1, FullName = "NAME" }
                             }.AsQueryable());
                    
            this.resource = new AteTestDetailResource
                                {
                                    ItemNumber = 1,
                                    NumberOfFails = 1,
                                    TestId = 1,
                                    PcbOperator = 1,
                                    PcbOperatorName = "NAME"
                                };

            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldAddBoardFailType()
        {
            this.AteTestDetailRepository.Received().Add(Arg.Any<AteTestDetail>());
        }

        [Test]
        public void ShouldReturnCreatedWithItemNumberOfOne()
        {
            this.result.Should().BeOfType<CreatedResult<AteTestDetail>>();
            var dataResult = ((CreatedResult<AteTestDetail>)this.result).Data;
            dataResult.ItemNumber.Should().Be(1);
            dataResult.TestId.Should().Be(1);
        }
    }
}