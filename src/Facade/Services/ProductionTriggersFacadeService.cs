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
        private readonly Domain.LinnApps.Repositories.IQueryRepository<ProductionTrigger> repository;

        private readonly IMasterRepository<PtlMaster> masterRepository;

        private readonly IRepository<Cit, string> citRepository;

        public ProductionTriggersFacadeService(Domain.LinnApps.Repositories.IQueryRepository<ProductionTrigger> repository, IRepository<Cit, string> citRepository, IMasterRepository<PtlMaster> masterRepository)
        {
            this.repository = repository;
            this.citRepository = citRepository;
            this.masterRepository = masterRepository;
        }

        public IResult<ProductionTriggersReport> GetProductionTriggerReport(string jobref, string citCode)
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

            var cit = this.citRepository.FindById(citCode);

            if (cit == null)
            {
                return new NotFoundResult<ProductionTriggersReport>($"cit {citCode} not found");
            }

            var ptlMaster = masterRepository.GetMasterRecord();

            var report = new ProductionTriggersReport(jobref, ptlMaster, cit, repository);

            return new SuccessResult<ProductionTriggersReport>(report);
        }
    }
}