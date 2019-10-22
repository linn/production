namespace Linn.Production.Domain.Tests.OverdueOrdersReportSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingOverdueOrdersReport : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            var overdueOrderLines = new List<OverdueOrderLine>
                                        {
                                            new OverdueOrderLine { JobId = 123, OrderRef = "555" },
                                            new OverdueOrderLine { JobId = 123, OrderRef = "556" }
                                        };

            this.OverdueOrderRepository.FilterBy(Arg.Any<Expression<Func<OverdueOrderLine, bool>>>())
                .Returns(overdueOrderLines.AsQueryable());

            this.result = this.Sut.OverdueOrdersReport(123, null, null, null, null, null, null);
        }

        [Test]
        public void ShouldCallAccountingCompanyRepository()
        {
            this.AccountingCompaniesRepository.Received().FindById("LINN");
        }

        [Test]
        public void ShouldGetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Outstanding Sales Orders by Days Late");
        }

        [Test]
        public void ShouldSetReportValues()
        {
            this.result.Rows.Should().HaveCount(2);
            var row1 = this.result.Rows.First();
            this.result.GetGridTextValue(row1.RowIndex, this.result.ColumnIndex("Order")).Should().Be("555");
        }
    }
}