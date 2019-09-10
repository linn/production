﻿namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRaisingWorksOrder : ContextBase
    {
        private WorksOrder result;

        private string department;

        private string partNumber;

        private string workStationCode;

        private int raisedBy;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "MAJIK";
            this.department = "DEPT";
            this.raisedBy = 33067;
            this.workStationCode = "STATION";

            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part { BomType = "A", AccountingCompany = "LINN" });

            this.WorkStationRepository.FindById(this.workStationCode).Returns(new WorkStation { WorkStationCode = this.workStationCode });

            this.WorksOrderService.CanRaiseWorksOrder(this.partNumber).Returns("SUCCESS");

            this.WorksOrderService.GetDepartment(this.partNumber, this.department).Returns("SUCCESS");

            this.ProductionTriggerLevelsRepository.FindById(this.partNumber).Returns(
                new ProductionTriggerLevel { PartNumber = this.partNumber, WsName = this.workStationCode });

            this.result = this.Sut.RaiseWorksOrder(new WorksOrder
                                                       {
                                                           PartNumber = this.partNumber,
                                                           RaisedByDepartment = this.department,
                                                           RaisedBy = this.raisedBy,
                                                           WorkStationCode = this.workStationCode
                                                       });
        }

        [Test]
        public void ShouldRaiseWorksOrder()
        {
            this.result.PartNumber.Should().Be(this.partNumber);
            this.result.RaisedByDepartment.Should().Be(this.department);
        }
    }
}
