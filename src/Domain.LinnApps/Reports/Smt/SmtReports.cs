namespace Linn.Production.Domain.LinnApps.Reports.Smt
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class SmtReports : ISmtReports
    {
        private readonly IRepository<WorksOrder, int> worksOrdersRepository;

        public SmtReports(IRepository<WorksOrder, int> worksOrdersRepository)
        {
            this.worksOrdersRepository = worksOrdersRepository;
        }

        public ResultsModel OutstandingWorksOrderParts(string smtLine, string[] parts)
        {
            throw new System.NotImplementedException();
        }
    }
}