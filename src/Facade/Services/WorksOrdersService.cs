namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrdersService : IWorksOrdersService
    {
        private readonly IRepository<WorksOrder, int> worksOrderRepository;

        private readonly ITransactionManager transactionManager;

        private readonly IWorksOrderFactory worksOrderFactory;

        private readonly IWorksOrderProxyService worksOrderProxyService;

        private readonly IProductAuditPack productAuditPack;

        public WorksOrdersService(
            IRepository<WorksOrder, int> worksOrderRepository,
            ITransactionManager transactionManager,
            IWorksOrderFactory worksOrderFactory,
            IWorksOrderProxyService worksOrderProxyService,
            IProductAuditPack productAuditPack)
        {
            this.worksOrderRepository = worksOrderRepository;
            this.transactionManager = transactionManager;
            this.worksOrderFactory = worksOrderFactory;
            this.worksOrderProxyService = worksOrderProxyService;
            this.productAuditPack = productAuditPack;
        }

        public IResult<WorksOrder> GetWorksOrder(int orderNumber)
        {
            var worksOrder = this.worksOrderRepository.FindById(orderNumber);

            if (worksOrder == null)
            {
                return new NotFoundResult<WorksOrder>();
            }

            return new SuccessResult<WorksOrder>(worksOrder);
        }

        public IResult<WorksOrder> AddWorksOrder(WorksOrderResource resource)
        {
            var worksOrder = this.CreateFromResource(resource);

            try
            {
                worksOrder = this.worksOrderFactory.RaiseWorksOrder(worksOrder);
            }
            catch (DomainException exception)
            {
                return new BadRequestResult<WorksOrder>(exception.Message);
            }

            this.worksOrderRepository.Add(worksOrder);

            this.worksOrderFactory.IssueSerialNumber(
                worksOrder.PartNumber,
                worksOrder.OrderNumber,
                worksOrder.DocType,
                worksOrder.RaisedBy,
                worksOrder.Quantity);

            try
            {
                this.productAuditPack.GenerateProductAudits(worksOrder.OrderNumber);
            }
            catch (Exception e)
            {
                return new BadRequestResult<WorksOrder>(e.Message);
            }

            this.transactionManager.Commit();

            return new CreatedResult<WorksOrder>(worksOrder);
        }

        public IResult<WorksOrder> UpdateWorksOrder(WorksOrderResource resource)
        {
            var worksOrder = this.worksOrderRepository.FindById(resource.OrderNumber);

            if (worksOrder == null)
            {
                return new NotFoundResult<WorksOrder>();
            }

            if (resource.ReasonCancelled != null)
            {
                try
                {
                    this.worksOrderFactory.CancelWorksOrder(worksOrder, resource.CancelledBy, resource.ReasonCancelled);
                }
                catch (DomainException exception)
                {
                    return new BadRequestResult<WorksOrder>(exception.Message);
                }

                return new SuccessResult<WorksOrder>(worksOrder);
            }

            this.UpdateFromResource(resource, worksOrder);

            return new SuccessResult<WorksOrder>(worksOrder);
        }

        public IResult<IEnumerable<WorksOrder>> SearchWorksOrders(string searchTerm)
        {
            return new SuccessResult<IEnumerable<WorksOrder>>(this.worksOrderRepository.FilterBy(w => w.PartNumber == searchTerm));
        }

        public IResult<WorksOrderDetails> GetWorksOrderDetails(string partNumber)
        {
            WorksOrderDetails worksOrderDetails;

            try
            {
                worksOrderDetails = this.worksOrderFactory.GetWorksOrderDetails(partNumber);
            }
            catch (DomainException exception)
            {
                return new BadRequestResult<WorksOrderDetails>(exception.Message);
            }

            return new SuccessResult<WorksOrderDetails>(worksOrderDetails);
        }

        private WorksOrder CreateFromResource(WorksOrderResource resource)
        {
            return new WorksOrder
                       {
                           PartNumber = resource.PartNumber,
                           OrderNumber = resource.OrderNumber,
                           CancelledBy = resource.CancelledBy,
                           ReasonCancelled = resource.ReasonCancelled,
                           RaisedBy = resource.RaisedBy,
                           RaisedByDepartment = resource.RaisedByDepartment,
                           DateCancelled = string.IsNullOrEmpty(resource.DateCancelled) ? (DateTime?)null : DateTime.Parse(resource.DateCancelled),
                           QuantityOutstanding = resource.QuantityOutstanding,
                           DocType = resource.DocType,
                           BatchNumber = resource.BatchNumber,
                           QuantityBuilt = resource.QuantityBuilt,
                           KittedShort = resource.KittedShort,
                           Outstanding = resource.Outstanding,
                           LabelsPrinted = resource.LabelsPrinted,
                           StartedByShift = resource.StartedByShift,
                           WorkStationCode = resource.WorkStationCode,
                           Quantity = resource.Quantity
                       };
        }

        private void UpdateFromResource(WorksOrderResource resource, WorksOrder worksOrder)
        {
            worksOrder.UpdateWorksOrder(resource.Quantity);
        }
    }
}
