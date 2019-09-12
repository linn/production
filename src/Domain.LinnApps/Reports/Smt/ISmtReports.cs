namespace Linn.Production.Domain.LinnApps.Reports.Smt
{
    using Linn.Common.Reporting.Models;

    public interface ISmtReports
    {
        ResultsModel OutstandingWorksOrderParts(string smtLine, string[] parts);
    }
}
