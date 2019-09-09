namespace Linn.Production.Facade.Services
{
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Persistence.LinnApps.Repositories;
    using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;

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
            var ptlMaster = this.masterRepository.GetMasterRecord();
            if (ptlMaster == null)
            {
                return new ServerFailureResult<ProductionTriggersReport>("Could not find PTL Master record");
            }

            var reportJobref = string.IsNullOrEmpty(jobref) ? ptlMaster.LastFullRunJobref : jobref;

            ProductionTriggerReportType triggerReportType;
            
            // if no cit then just pick the first production one you can find
            var cit = (string.IsNullOrEmpty(citCode)) ? this.citRepository.FilterBy(c => c.BuildGroup == "PP" && c.DateInvalid == null).ToList().OrderBy(c => c.SortOrder).FirstOrDefault() : this.citRepository.FindById(citCode);

            if (cit == null)
            {
                return new NotFoundResult<ProductionTriggersReport>($"cit {citCode} not found");
            }

            var report = new ProductionTriggersReport(reportJobref, ptlMaster, cit, repository);

            return new SuccessResult<ProductionTriggersReport>(report);
        }
    }
}