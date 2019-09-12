namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRaisingWorksOrderWhenNoDepartment : ContextBase
    {
        private Action action;

        private string department;

        private string partNumber;

        private int raisedBy;

        private string workStationCode;

        private string citCode;

        private string citDepartment;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "MAJIK";
            this.department = "DEPT";
            this.raisedBy = 33067;
            this.workStationCode = "STATION";
            this.citCode = "AB";
            this.citDepartment = "DEPT2";

            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part { BomType = "A", AccountingCompany = "LINN" });

            this.WorksOrderService.CanRaiseWorksOrder(this.partNumber).Returns("SUCCESS");

            this.ProductionTriggerLevelsRepository.FindById(this.partNumber).Returns(
                new ProductionTriggerLevel { PartNumber = this.partNumber, WsName = this.workStationCode, CitCode = this.citCode });

            this.CitRepository.FindById(this.citCode)
                .Returns(new Cit { Code = this.citCode, DepartmentCode = this.citDepartment });

            this.DepartmentRepository.FindById(this.citDepartment)
                .Returns((Department)null);

            this.action = () => this.Sut.RaiseWorksOrder(new WorksOrder
                                                             {
                                                                 PartNumber = this.partNumber,
                                                                 RaisedByDepartment = this.department,
                                                                 RaisedBy = this.raisedBy,
                                                                 WorkStationCode = this.workStationCode
                                                             });
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<DomainException>().WithMessage($"Department code not found for CIT {this.citCode}");
        }
    }
}