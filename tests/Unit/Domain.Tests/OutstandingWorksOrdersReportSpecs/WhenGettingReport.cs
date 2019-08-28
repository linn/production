namespace Linn.Production.Domain.Tests.OutstandingWorksOrdersReportSpecs
{
    using System;
    using System.Data;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        private ResultsModel results;

        [SetUp]
        public void SetUp()
        {
            var resultTable = new DataTable("name")
                                        {
                                            Columns =
                                                {
                                                    new DataColumn("ORDER_NUMBER", typeof(int)),
                                                    new DataColumn("PART_NUMBER", typeof(string)),
                                                    new DataColumn("QTY_OUTSTANDING", typeof(string)),
                                                    new DataColumn("DATE_RAISED", typeof(DateTime)),
                                                    new DataColumn("CIT_CODE", typeof(string)),
                                                    new DataColumn("DESCRIPTION", typeof(string)),
                                                    new DataColumn("WORK_STATION_CODE", typeof(string))
                                                }
                                        };

            var newRow = resultTable.NewRow();
            newRow["ORDER_NUMBER"] = 12345;
            newRow["PART_NUMBER"] = "part";
            newRow["QTY_OUTSTANDING"] = "qty";
            newRow["DATE_RAISED"] = new DateTime();
            newRow["CIT_CODE"] = "code";
            newRow["DESCRIPTION"] = "desc";
            newRow["WORK_STATION_CODE"] = "wscode";

            resultTable.Rows.Add(newRow);

            this.DatabaseService.GetReport().Returns(resultTable);

            this.results = this.Sut.GetOutstandingWorksOrders();
        }

        [Test]
        public void ShouldGetData()
        {
            this.DatabaseService.Received().GetReport();
        }

        [Test]
        public void ShouldReturnResults()
        {
            this.results.Rows.Should().HaveCount(1);
            this.results.Rows.ElementAt(0).RowId.Should().Be("12345");
        }
    }
}
