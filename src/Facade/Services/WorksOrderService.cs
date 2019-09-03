namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Resources;

    public class WorksOrderService : IWorksOrderService
    {
        private readonly IRepository<WorksOrder, int> worksOrderRepository;

        private readonly ITransactionManager transactionManager;

        private readonly IFacadeService<Part, string, PartResource, PartResource> partsService;

        private readonly IGetNextBatchService getNextBatchService;

        private readonly ICanRaiseWorksOrderService canRaiseWorksOrderService;

        private readonly IGetDepartmentService getDepartmentService;

        public WorksOrderService(
            IRepository<WorksOrder, int> worksOrderRepository,
            ITransactionManager transactionManager,
            IFacadeService<Part, string, PartResource, PartResource> partsService,
            IGetNextBatchService getNextBatchService,
            ICanRaiseWorksOrderService canRaiseWorksOrderService, 
            IGetDepartmentService getDepartmentService)
        {
            this.worksOrderRepository = worksOrderRepository;
            this.transactionManager = transactionManager;
            this.partsService = partsService;
            this.getNextBatchService = getNextBatchService;
            this.canRaiseWorksOrderService = canRaiseWorksOrderService;
            this.getDepartmentService = getDepartmentService;
        }

        public IResult<WorksOrder> GetWorksOrder(int orderNumber)
        {
            throw new NotImplementedException();
        }

        public IResult<WorksOrder> AddWorksOrder(WorksOrderResource resource)
        {
            var worksOrder = this.CreateFromResource(resource);

            var partResult = this.partsService.GetById(resource.PartNumber);

            // TODO bung all this out to the domain - pass in everything needed then do the checks
            // So would be a domain service that this calls?

            if (partResult.GetType() == typeof(NotFoundResult<Part>))
            {
                return new BadRequestResult<WorksOrder>($"No matching part found for Part Number {resource.PartNumber}");
            }

            var part = ((SuccessResult<Part>)partResult).Data;

            if (part.BomType == null)
            {
                return new BadRequestResult<WorksOrder>($"No matching part found for Part Number {resource.PartNumber}");
            }

            if (part.BomType == "P")
            {
                return new BadRequestResult<WorksOrder>($"Cannot raise a works order for phantom part {resource.PartNumber}");
            }

            var batchNumber2 = this.getNextBatchService.GetNextBatch(resource.PartNumber);

            if (this.RebuildPart(resource.PartNumber))
            {
                return new BadRequestResult<WorksOrder>($"Use Works Order Rebuild Utility for this part {resource.PartNumber}");
            }

            var accountingCompany = part.AccountingCompany;

            if (accountingCompany == "LINN")
            {
                var canRaiseWorksOrder = this.canRaiseWorksOrderService.CanRaiseWorksOrder(resource.PartNumber);

                if (canRaiseWorksOrder != "SUCCESS")
                {
                    return new BadRequestResult<WorksOrder>(canRaiseWorksOrder);
                }

                var departmentMessage = this.getDepartmentService.GetDepartment(
                    resource.PartNumber,
                    resource.RaisedByDepartment);

                // TODO test in the domain service
                if (departmentMessage != "SUCCESS")
                {
                    return new BadRequestResult<WorksOrder>(departmentMessage);
                }
            }

            // TODO get new wo number from repo
            this.worksOrderRepository.Add(worksOrder);
            this.transactionManager.Commit();

            return new CreatedResult<WorksOrder>(worksOrder);
        }

        public IResult<WorksOrder> UpdateWorksOrder(WorksOrderResource resource)
        {
            throw new NotImplementedException();
        }

        public IResult<IEnumerable<WorksOrder>> SearchWorksOrders(string searchTerm)
        {
            throw new NotImplementedException();
        }

        private bool RebuildPart(string partNumber)
        {
            var partNumbers = new string[] { "ASAKA/R", "TROIKA/R", "ARKIV/R", "KARMA/R", "KLYDE/R", "NEW ARKIV/R" };

            return partNumbers.Contains(partNumber);
        }

        private WorksOrder CreateFromResource(WorksOrderResource resource)
        {
            // TODO try catch domain exceptions
            // TODO get the new order number
            return new WorksOrder
                       {
                           OrderNumber = resource.OrderNumber,
                           CancelledBy = resource.CancelledBy,
                           PartNumber = resource.PartNumber,
                           QuantityOutstanding = resource.QuantityOutstanding,
                           ReasonCancelled = resource.ReasonCancelled,
                           RaisedByDepartment = resource.RaisedByDepartment,
                           KittedShort = resource.KittedShort,
                           DateCancelled =
                               string.IsNullOrEmpty(resource.DateCancelled)
                                   ? (DateTime?)null
                                   : DateTime.Parse(resource.DateCancelled),
                           LabelsPrinted = resource.LabelsPrinted,
                           StartedByShift = resource.StartedByShift,
                           QuantityBuilt = resource.QuantityBuilt,
                           WorkStationCode = resource.WorkStationCode,
                           BatchNumber = resource.BatchNumber,
                           Outstanding = resource.Outstanding,
                           DateRaised = DateTime.Parse(resource.DateRaised)
                       };
        }
    }
}
