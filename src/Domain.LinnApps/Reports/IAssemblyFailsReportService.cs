namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    public interface IAssemblyFailsReportService
    {
        ResultsModel GetAssemblyFailsWaitingListReport();

        ResultsModel GetAssemblyFailsMeasuresReport(DateTime fromDate, DateTime toDate, AssemblyFailGroupBy groupBy);
    }
}