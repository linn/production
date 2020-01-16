namespace Linn.Production.Domain.Tests.AssemblyFailsReportsSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NUnit.Framework;

    public class WhenGettingMeasuresByBoardPartNumberWithBlanks : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            this.AssemblyFails.Add(new AssemblyFail
                                       {
                                           Id = 4,
                                           DateTimeFound = 2.June(2020),
                                           BoardPartNumber = null,
                                           NumberOfFails = 2,
                                           FaultCode = new AssemblyFailFaultCode { FaultCode = "F1", Description = "Fault 1" },
                                           CitResponsible = new Cit { Code = "C", Name = "Cit 1" },
                                           CircuitPart = "Circuit Part 1",
                                           WorksOrder = new WorksOrder { OrderNumber = 45, PartNumber = "W O Part" },
                                           PersonResponsible = new Employee { Id = 1, FullName = "CitName" }
                                       });
            this.AssemblyFails.Add(new AssemblyFail
                                       {
                                           Id = 5,
                                           DateTimeFound = 2.June(2020),
                                           BoardPartNumber = string.Empty,
                                           NumberOfFails = 2,
                                           FaultCode = new AssemblyFailFaultCode { FaultCode = "F1", Description = "Fault 1" },
                                           CitResponsible = new Cit { Code = "C", Name = "Cit 1" },
                                           CircuitPart = "Circuit Part 1",
                                           WorksOrder = new WorksOrder { OrderNumber = 45, PartNumber = "W O Part" },
                                           PersonResponsible = new Employee { Id = 1, FullName = "CitName" }
                                       });
            this.result = this.Sut.GetAssemblyFailsMeasuresReport(
                1.June(2020),
                30.June(2020),
                AssemblyFailGroupBy.BoardPartNumber);
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Assembly Fails Measures Grouped By Board Part Number");
        }

        [Test]
        public void ShouldReturnValuesExcludingFailsWithBlankFields()
        {
            this.result.Rows.Should().HaveCount(2);
            this.result.Columns.Should().HaveCount(4);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("Board 1/1"), this.result.ColumnIndex("20")).Should().Be(3);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("Board 1/1"), this.result.ColumnIndex("Total")).Should().Be(3);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("Board 2"), this.result.ColumnIndex("21")).Should().Be(2);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("Board 2"), this.result.ColumnIndex("Total")).Should().Be(2);
            this.result.GetZeroPaddedTotalValue(this.result.ColumnIndex("20")).Should().Be(3);
            this.result.GetZeroPaddedTotalValue(this.result.ColumnIndex("21")).Should().Be(2);
            this.result.GetZeroPaddedTotalValue(this.result.ColumnIndex("Total")).Should().Be(5);
        }
    }
}
