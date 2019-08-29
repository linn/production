namespace Linn.Production.Facade.Services
{
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Persistence.LinnApps.Repositories;

    public class ProductionTriggersFacadeService : IProductionTriggersFacadeService
    {
        private readonly IQueryRepository<ProductionTrigger> repository;

        private readonly IMasterRepository<PtlMaster> masterRepository;

        private readonly IRepository<Cit, string> citRepository;

        public ProductionTriggersFacadeService(IQueryRepository<ProductionTrigger> repository, IRepository<Cit, string> citRepository, IMasterRepository<PtlMaster> masterRepository)
        {
            this.repository = repository;
            this.citRepository = citRepository;
            this.masterRepository = masterRepository;
        }

        public IResult<ProductionTriggersReport> GetProductionTriggerReport(string jobref, string citCode, string reportType)
        {
            if (string.IsNullOrEmpty(jobref))
            {
                return new BadRequestResult<ProductionTriggersReport>("You must supply a jobref");
            }

            if (string.IsNullOrEmpty(citCode))
            {
                return new BadRequestResult<ProductionTriggersReport>("You must supply a citCode");
            }

            ProductionTriggerReportType triggerReportType;

            switch (reportType)
            {
                case "Brief":
                    triggerReportType = ProductionTriggerReportType.Brief;
                    break;
                case "Full":
                    triggerReportType = ProductionTriggerReportType.Full;
                    break;
                default:
                    return new BadRequestResult<ProductionTriggersReport>("Invalid report type");
            }

            var cit = this.citRepository.FindById(citCode);

            if (cit == null)
            {
                return new NotFoundResult<ProductionTriggersReport>($"cit {citCode} not found");
            }

            var ptlMaster = masterRepository.GetMasterRecord();

            var triggers = (triggerReportType == ProductionTriggerReportType.Full)
                ? repository.FilterBy(t => t.Jobref == jobref && t.Citcode == citCode)
                : repository.FilterBy(t => t.Jobref == jobref && t.Citcode == citCode && t.ReportType == "BRIEF");

            triggers = triggers.OrderBy(t => t.SortOrder).ThenBy(t => t.EarliestRequestedDate);

            var report = new ProductionTriggersReport()
            {
                ReportType = triggerReportType,
                PtlMaster = ptlMaster.LastFullRunJobref == jobref ? ptlMaster : null,
                Cit = cit,
                Triggers = triggers
            };

            return new SuccessResult<ProductionTriggersReport>(report);
        }
    }
}