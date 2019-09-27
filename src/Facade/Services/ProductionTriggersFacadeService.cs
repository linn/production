namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Repositories;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Persistence.LinnApps.Repositories;

    public class ProductionTriggersFacadeService : IProductionTriggersFacadeService
    {
        private readonly IQueryRepository<ProductionTrigger> repository;

        private readonly ISingleRecordRepository<PtlMaster> masterRepository;

        private readonly IRepository<Cit, string> citRepository;

        private readonly IRepository<WorksOrder, int> worksOrderRepository;

        public ProductionTriggersFacadeService(IQueryRepository<ProductionTrigger> repository, IRepository<Cit, string> citRepository, ISingleRecordRepository<PtlMaster> masterRepository, IRepository<WorksOrder, int> worksOrderRepository)
        {
            this.repository = repository;
            this.citRepository = citRepository;
            this.masterRepository = masterRepository;
            this.worksOrderRepository = worksOrderRepository;
        }

        public IResult<ProductionTriggersReport> GetProductionTriggerReport(string jobref, string citCode)
        {
            var ptlMaster = this.masterRepository.GetRecord();
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

            var report = new ProductionTriggersReport(reportJobref, ptlMaster, cit, this.repository);

            return new SuccessResult<ProductionTriggersReport>(report);
        }

        public IResult<ProductionTriggerFacts> GetProductionTriggerFacts(string jobref, string partNumber)
        {
            if (string.IsNullOrEmpty(jobref))
            {
                return new BadRequestResult<ProductionTriggerFacts>("Must supply a jobref");
            }

            if (string.IsNullOrEmpty(partNumber))
            {
                return new BadRequestResult<ProductionTriggerFacts>("Must supply a part number");
            }

            var trigger = this.repository.FindBy(f => f.Jobref == jobref && f.PartNumber == partNumber);

            if (trigger == null)
            {
                return new NotFoundResult<ProductionTriggerFacts>("No facts found for that jobref and part number");
            }

            var facts = new ProductionTriggerFacts(trigger);
            facts.OutstandingWorksOrders = trigger.QtyBeingBuilt > 0
                ? this.worksOrderRepository.FilterBy(w =>
                    w.PartNumber == partNumber & w.DateCancelled == null & w.Quantity > w.QuantityBuilt).ToList()
                : new List<WorksOrder>();

            return new SuccessResult<ProductionTriggerFacts>(facts);
        }
    }
}