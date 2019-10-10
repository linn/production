namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class OrdersReports : IOrdersReports
    {
        private readonly IQueryRepository<MCDLine> mcdRepository;

        public OrdersReports(IQueryRepository<MCDLine> mcdRepository)
        {
            this.mcdRepository = mcdRepository;
        }

        public ManufacturingCommitDateResults ManufacturingCommitDate(string date)
        {
            throw new System.NotImplementedException();
        }
    }
}