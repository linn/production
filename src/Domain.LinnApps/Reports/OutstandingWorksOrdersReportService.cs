namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Data;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Extensions;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class OutstandingWorksOrdersReportService : IOutstandingWorksOrdersReportService
    {
        private readonly IOutstandingWorksOrdersReportDatabaseService databaseService;

        public OutstandingWorksOrdersReportService(IOutstandingWorksOrdersReportDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ResultsModel GetOutstandingWorksOrders(string reportType, string searchParameter)
        {
            var table = this.databaseService.GetReport(reportType, searchParameter);

            var results =
                new ResultsModel(
                    new[]
                        {
                            "Part Number",
                            "Description",
                            "Date Raised",
                            "Qty Outstanding"
                        })
                    {
                        RowHeader = "Order Number",
                        ReportTitle = new NameModel("Outstanding Works Orders")
                    };

            for (var i = 0; i < 3; i++)
            {
                results.SetColumnType(i, GridDisplayType.TextValue);
            }

            foreach (DataRow tableRow in table.Rows)
            {
                var row = results.AddRow(tableRow[0]?.ToString());

                results.SetGridTextValue(row.RowIndex, 0, tableRow[1]?.ToString());
                results.SetGridTextValue(row.RowIndex, 1, tableRow[5]?.ToString());
                results.SetGridTextValue(row.RowIndex, 2, tableRow[3]?.ToString());
                results.SetGridValue(row.RowIndex, 3, tableRow[2]?.ToString().ParseDecimal());
            }

            return results;
        }
    }
}
