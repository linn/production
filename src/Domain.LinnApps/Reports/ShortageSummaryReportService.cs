namespace Linn.Production.Domain.LinnApps.Reports
{
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

        public ShortageSummaryReportService(IRepository<AccountingCompany, string> accountingCompaniesRepository, ISingleRecordRepository<PtlMaster> masterRepository, IQueryRepository<ProductionTrigger> repository, IRepository<Cit, string> citRepository, IQueryRepository<ProductionBackOrder> backOrderRepository, IQueryRepository<WswShortage> shortageRepository)
        {
            this.accountingCompaniesRepository = accountingCompaniesRepository;
            this.masterRepository = masterRepository;
            this.triggerRepository = repository;
            this.citRepository = citRepository;
            this.backOrderRepository = backOrderRepository;
            this.shortageRepository = shortageRepository;
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
                throw new DomainException($"Could not CIT with code {citCode}");
            }

            var linnCompany = this.accountingCompaniesRepository.FindById("LINN");
            if (linnCompany == null)
            {
                throw new DomainException($"Could not find Linn Accounting Company");
            }

            var report = new ProductionTriggersReport(ptlJobref, ptlMaster, cit, this.triggerRepository);
            var backOrders =
                this.backOrderRepository.FilterBy(b => b.JobId == linnCompany.LatestSosJobId && b.CitCode == citCode);
            var wswShortages = this.shortageRepository.FilterBy(s => s.Jobref == ptlJobref && s.CitCode == citCode);

            var summary = new ShortageSummary();
            var shortages = new List<ShortageResult>();

            foreach (var trigger in report.Triggers.Where(t => t.Priority == "1" || t.Priority == "2" ))
            {
                summary.OnesTwos++;
                if (trigger.IsShortage())
                {
                    summary.NumShortages++;

                    var shortage = new ShortageResult();
                    shortage.Priority = trigger.Priority;
                    shortage.PartNumber = trigger.PartNumber;
                    shortage.Build = trigger.ReqtForInternalAndTriggerLevelBT;
                    shortage.CanBuild = trigger.CanBuild;
                    shortage.Kanban = trigger.KanbanSize;

                    var shortageBackOrders = backOrders.Where(o => o.ArticleNumber == shortage.PartNumber);
                    if (shortageBackOrders.Any())
                    {
                        shortage.BackOrderQty = shortageBackOrders.Sum(o => o.BackOrderQty);
                        shortage.EarliestRequestedDate = shortageBackOrders.Min(o => o.RequestedDeliveryDate);
                    }

                    var details = wswShortages.Where(w => w.PartNumber == shortage.PartNumber);

                    var model = new ResultsModel();
                    model.AddColumn("shortPartNumber", "Short Part Number");
                    model.AddColumn("description", "Description");
                    model.AddColumn("category", "Cat");
                    model.AddColumn("reqt", "Reqt");
                    model.AddColumn("stock", "Stock");
                    model.AddColumn("avail", "Avail");
                    model.AddColumn("res", "Res");
                    model.AddColumn("canBuild", "Can Build");
                    model.AddColumn("notes", "Notes");

                    foreach (var detail in details)
                    {
                        var row = model.AddRow(detail.ShortPartNumber);
                        model.SetGridTextValue(row.RowIndex, model.ColumnIndex("shortPartNumber"), detail.ShortPartNumber);
                        model.SetGridTextValue(row.RowIndex, model.ColumnIndex("description"), detail.ShortPartDescription);
                        model.SetGridTextValue(row.RowIndex, model.ColumnIndex("category"), detail.ShortageCategory);
                        model.SetGridTextValue(row.RowIndex, model.ColumnIndex("reqt"), detail.Required.ToString());
                        model.SetGridTextValue(row.RowIndex, model.ColumnIndex("stock"), detail.Stock.ToString());
                        model.SetGridTextValue(row.RowIndex, model.ColumnIndex("avail"), detail.AdjustedAvailable.ToString());
                        model.SetGridTextValue(row.RowIndex, model.ColumnIndex("res"), detail.QtyReserved.ToString());
                        model.SetGridTextValue(row.RowIndex, model.ColumnIndex("canBuild"), detail.CanBuild.ToString());
                   //     model.SetGridTextValue(row.RowIndex, model.ColumnIndex("notes"), summary.FiveDay.ToString());
                    }

                    shortage.Results = model;

                    shortages.Add(shortage);
                }
            }

            summary.Shortages = shortages;

            return summary;
        }
    }
}