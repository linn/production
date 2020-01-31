namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Collections.Generic;

    using Linn.Common.Reporting.Models;

    public interface IFailsReportService
    {
        IEnumerable<ResultsModel> FailedPartsReport(string citCode);
    }
}