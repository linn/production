namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Data;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class OutstandingWorksOrdersReportService : IOutstandingWorksOrdersReportService
    {
        private readonly IOutstandingWorksOrdersReportDatabaseService databaseService;

        public OutstandingWorksOrdersReportService(IOutstandingWorksOrdersReportDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ResultsModel GetOutstandingWorksOrders()
        {
            var table = this.databaseService.GetReport();

            var results =
                new ResultsModel(
                    new[]
                        {
                            "Order Number",
                            "Part Number",
                            "Description",
                            "Date Raised",
                            "Qty Outstanding"
                        })
                    {
                        RowHeader = "Order Number",
                        ReportTitle = new NameModel("Outstanding Works Orders")
                    };

            var rowId = 0;

            foreach (DataRow tableRow in table.Rows)
            {
                var row = results.AddRow((rowId++).ToString());

                results.SetGridTextValue(row.RowIndex, 0, tableRow[0]?.ToString());
                results.SetGridTextValue(row.RowIndex, 1, tableRow[1]?.ToString());
                results.SetGridTextValue(row.RowIndex, 2, tableRow[5]?.ToString());
                results.SetGridTextValue(row.RowIndex, 3, tableRow[3]?.ToString());
                results.SetGridTextValue(row.RowIndex, 4, tableRow[2]?.ToString());
            }

            return results;
        }
    }
}
