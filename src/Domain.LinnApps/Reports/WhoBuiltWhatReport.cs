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
                model.ReportTitle = new NameModel(user.Key.UserName);
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
    }
}