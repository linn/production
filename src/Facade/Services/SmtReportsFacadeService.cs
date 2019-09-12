namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports.Smt;

    public class SmtReportsFacadeService : ISmtReportsFacadeService
    {
        private readonly ISmtReports smtReports;

        public SmtReportsFacadeService(ISmtReports smtReports)
        {
            this.smtReports = smtReports;
        }

        public IResult<ResultsModel> GetPartsForOutstandingWorksOrders(string smtLine, string[] parts)
        {
            return new SuccessResult<ResultsModel>(
                this.smtReports.OutstandingWorksOrderParts(smtLine, parts));
        }
    }
}