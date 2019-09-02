namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;

    using Linn.Common.Reporting.Models;

    public interface IAssemblyFailsReportService
    {
        ResultsModel GetAssemblyFailsWaitingListReport();

        ResultsModel GetAssemblyFailsMeasuresReport(DateTime fromDate, DateTime toDate, AssemblyFailGroupBy groupBy);
    }
}