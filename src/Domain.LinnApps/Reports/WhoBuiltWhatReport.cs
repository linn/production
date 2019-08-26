namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class WhoBuiltWhatReport : IWhoBuiltWhatReport
    {
        private readonly IRepository<WhoBuiltWhat, string> repository;

        private readonly IReportingHelper helper;

        public WhoBuiltWhatReport(IRepository<WhoBuiltWhat, string> repository, IReportingHelper helper)
        {
            this.repository = repository;
            this.helper = helper;
        }

        public IEnumerable<ResultsModel> WhoBuiltWhat(string fromDate, string toDate, string citCode)
        {
            var from = DateTime.Parse(fromDate);
            var to = DateTime.Parse(toDate);
            var results = this.repository
                .FilterBy(a => a.CitCode == citCode && a.SernosDate >= from && a.SernosDate <= to).ToList();

            var returnResults = new List<ResultsModel>();
            var displaySequence = 0;
            foreach (var user in results.GroupBy(a => new { a.CreatedBy, a.UserName }).OrderBy(b => b.Key.UserName))
            {
                var model = new ResultsModel();

                model.AddColumn("qty", "Qty Built");
                model.ReportTitle = new NameModel(user.Key.UserName)
                                        {
                                            DrillDownList = new List<DrillDownModel>
                                                                {
                                                                    new DrillDownModel(
                                                                        "Details",
                                                                        $"/production/reports/who-built-what-details?userNumber={user.Key.CreatedBy}&fromDate={fromDate}&toDate={toDate}&citCode={citCode}")
                                                                }
                                        };
                model.RowHeader = "Part Number Built";
                model.DisplaySequence = displaySequence++;
                var values = results
                    .Where(a => a.CreatedBy == user.Key.CreatedBy)
                    .Select(whoBuiltWhat => new CalculationValueModel
                                                {
                                                    Quantity = whoBuiltWhat.QtyBuilt,
                                                    RowId = whoBuiltWhat.ArticleNumber,
                                                    ColumnId = "qty"
                                                }).ToList();

                this.helper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);
                returnResults.Add(model);
            }

            return returnResults;
        }

        public ResultsModel WhoBuiltWhatDetails(string fromDate, string toDate, int userNumber)
        {
            var from = DateTime.Parse(fromDate);
            var to = DateTime.Parse(toDate);
            var results = this.repository
                .FilterBy(a => a.CreatedBy == userNumber && a.SernosDate >= from && a.SernosDate <= to).ToList();

            var model = new ResultsModel();

            model.AddColumn("articleNumber", "Article Number");
            model.AddColumn("sernosNumber", "Serial Number");
            model.AddColumn("sernosDate", "Date Built");
            model.ReportTitle = new NameModel(
                $"Products built by {results.First().UserName} between {from.ToString("dd-MMM-yyyy")} and {to.ToString("dd-MMM-yyyy")}");
            model.RowHeader = "Part Number Built";

            foreach (var wbw in results.OrderBy(a => a.ArticleNumber).ThenBy(b => b.SernosNumber))
            {
                var row = model.AddRow(wbw.SernosNumber.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("articleNumber"), wbw.ArticleNumber);
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("sernosNumber"), wbw.SernosNumber.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("sernosDate"), wbw.SernosDate?.ToString("dd-MMM-yyyy"));
            }

            return model;
        }
    }
}