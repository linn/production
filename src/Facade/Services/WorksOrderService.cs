namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrderService : IWorksOrderService
    {
        private readonly IRepository<WorksOrder, int> worksOrderRepository;

        private readonly ITransactionManager transactionManager;

        private readonly IWorksOrderFactory worksOrderFactory;

        private readonly IWorksOrderProxyService worksOrderProxyService;

        public WorksOrderService(
            IRepository<WorksOrder, int> worksOrderRepository,
            ITransactionManager transactionManager,
            IWorksOrderFactory worksOrderFactory,
            IWorksOrderProxyService worksOrderProxyService)
        {
            this.worksOrderRepository = worksOrderRepository;
            this.transactionManager = transactionManager;
            this.worksOrderFactory = worksOrderFactory;
            this.worksOrderProxyService = worksOrderProxyService;
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
            WorksOrder worksOrder;

            try
            {
                worksOrder = this.worksOrderFactory.RaiseWorksOrder(resource.PartNumber, resource.RaisedByDepartment, resource.RaisedBy);
            }
            catch (DomainException exception)
            {
                return new BadRequestResult<WorksOrder>(exception.Message);
            }

            // TODO orderNumber...
            this.worksOrderRepository.Add(worksOrder);

            this.UpdateFromResource(resource, worksOrder);

            this.worksOrderFactory.IssueSerialNumber(
                worksOrder.PartNumber,
                worksOrder.OrderNumber,
                worksOrder.DocType,
                worksOrder.RaisedBy,
                worksOrder.QuantityOutstanding);

            this.transactionManager.Commit();

            return new CreatedResult<WorksOrder>(worksOrder);
        }

        public IResult<WorksOrder> CancelWorksOrder(WorksOrderResource resource)
        {
            var worksOrder = this.worksOrderRepository.FindById(resource.OrderNumber);

            if (worksOrder == null)
            {
                return new NotFoundResult<WorksOrder>();
            }

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

        // TODO test these
        public IResult<IEnumerable<WorksOrder>> SearchWorksOrders(string searchTerm)
        {
            return new SuccessResult<IEnumerable<WorksOrder>>(this.worksOrderRepository.FilterBy(w => w.PartNumber == searchTerm));
        }

        public IResult<string> GetAuditDisclaimer(string partNumber)
        {
            return new SuccessResult<string>(this.worksOrderProxyService.GetAuditDisclaimer());
        }

        private void UpdateFromResource(WorksOrderResource resource, WorksOrder worksOrder)
        {
            worksOrder.UpdateWorksOrder(
                resource.BatchNumber,
                resource.CancelledBy,
                string.IsNullOrEmpty(resource.DateCancelled) ? (DateTime?)null : DateTime.Parse(resource.DateCancelled),
                resource.KittedShort,
                resource.LabelsPrinted,
                resource.Outstanding,
                resource.QuantityOutstanding,
                resource.QuantityBuilt,
                resource.ReasonCancelled,
                resource.StartedByShift,
                resource.DocType,
                resource.WorkStationCode);
        }
    }
}
