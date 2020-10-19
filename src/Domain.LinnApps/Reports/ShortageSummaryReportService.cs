namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.Triggers;

    public class ShortageSummaryReportService : IShortageSummaryReportService
    {
        private readonly IRepository<AccountingCompany, string> accountingCompaniesRepository;

        private readonly ISingleRecordRepository<PtlMaster> masterRepository;

        private readonly IQueryRepository<ProductionTrigger> triggerRepository;

        private readonly IRepository<Cit, string> citRepository;

        private readonly IQueryRepository<ProductionBackOrder> backOrderRepository;

        private readonly IQueryRepository<WswShortage> shortageRepository;

        private readonly IQueryRepository<WswShortageStory> shortageStoryRepository;

        public ShortageSummaryReportService(IRepository<AccountingCompany, string> accountingCompaniesRepository,
            ISingleRecordRepository<PtlMaster> masterRepository, IQueryRepository<ProductionTrigger> repository,
            IRepository<Cit, string> citRepository, IQueryRepository<ProductionBackOrder> backOrderRepository,
            IQueryRepository<WswShortage> shortageRepository, IQueryRepository<WswShortageStory> shortageStoryRepository)
        {
            this.accountingCompaniesRepository = accountingCompaniesRepository;
            this.masterRepository = masterRepository;
            this.triggerRepository = repository;
            this.citRepository = citRepository;
            this.backOrderRepository = backOrderRepository;
            this.shortageRepository = shortageRepository;
            this.shortageStoryRepository = shortageStoryRepository;
        }

        public ShortageSummary ShortageSummaryByCit(string citCode, string ptlJobref)
        {
            var ptlMaster = this.masterRepository.GetRecord();
            if (ptlMaster == null)
            { 
                throw new DomainException("Could not find PTL Master record");
            }

            var cit = this.citRepository.FindById(citCode);
            if (cit == null)
            {
                throw new DomainException($"Could not find CIT with code {citCode}");
            }

            var linnCompany = this.accountingCompaniesRepository.FindById("LINN");
            if (linnCompany == null)
            {
                throw new DomainException($"Could not find Linn Accounting Company");
            }

            var report = new ProductionTriggersReport(ptlJobref, ptlMaster, cit, this.triggerRepository);
            var backOrders =
                this.backOrderRepository.FilterBy(b => b.JobId == linnCompany.LatestSosJobId && b.CitCode == citCode).ToList();
            var wswShortages = this.shortageRepository.FilterBy(s => s.Jobref == ptlJobref && s.CitCode == citCode).ToList();
            var wswShortagesStories = this.shortageStoryRepository.FilterBy(s => s.Jobref == ptlJobref && s.CitCode == citCode).ToList();

            var summary = new ShortageSummary();
            var shortages = new List<ShortageResult>();

            summary.CitName = cit.Name;

            foreach (var trigger in report.Triggers.Where(t => t.Priority == "1" || t.Priority == "2" ))
            {
                summary.OnesTwos++;
                if (trigger.IsShortage())
                {
                    var shortage = new ShortageResult();
                    shortage.Priority = trigger.Priority;
                    shortage.PartNumber = trigger.PartNumber;
                    shortage.Build = trigger.ReqtForInternalAndTriggerLevelBT;
                    if (trigger.QtyBeingBuilt > 0)
                    {
                        shortage.Build += trigger.QtyBeingBuilt;
                    }

                    shortage.CanBuild = trigger.CanBuild;
                    shortage.Kanban = trigger.KanbanSize;

                    var shortageBackOrders = backOrders.Where(o => o.ArticleNumber == shortage.PartNumber).ToList();
                    if (shortageBackOrders.Any())
                    {
                        shortage.BackOrderQty = shortageBackOrders.Sum(o => o.BackOrderQty);
                        shortage.EarliestRequestedDate = shortageBackOrders.Min(o => o.RequestedDeliveryDate);
                    }

                    var details = wswShortages.Where(w => w.PartNumber == shortage.PartNumber).GroupBy(w => w.ShortPartNumber).Select(w => w.First());

                    foreach (var detail in details)
                    {
                        shortage.AddWswShortage(detail);

                        var stories = wswShortagesStories.Where(w => w.ShortPartNumber == detail.ShortPartNumber && w.PartNumber == detail.PartNumber);
                        foreach (var story in stories)
                        {
                            shortage.AddWswShortageStory(story);
                        }
                    }

                    shortages.Add(shortage);
                }
            }

            summary.Shortages = shortages;

            return summary;
        }
    }
}