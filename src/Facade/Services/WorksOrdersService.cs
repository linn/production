namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.Extensions;
    using Linn.Production.Resources;

    public class WorksOrdersService : FacadeService<WorksOrder, int, WorksOrderResource, WorksOrderResource>, IWorksOrdersService
    {
        private readonly IRepository<WorksOrder, int> worksOrderRepository;

        private readonly ITransactionManager transactionManager;

        private readonly IWorksOrderFactory worksOrderFactory;

        private readonly IWorksOrderUtilities worksOrderUtilities;

        public WorksOrdersService(
            IRepository<WorksOrder, int> worksOrderRepository,
            ITransactionManager transactionManager,
            IWorksOrderFactory worksOrderFactory, 
            IWorksOrderUtilities worksOrderUtilities)
            : base(worksOrderRepository, transactionManager)
        {
            this.worksOrderRepository = worksOrderRepository;
            this.transactionManager = transactionManager;
            this.worksOrderFactory = worksOrderFactory;
            this.worksOrderUtilities = worksOrderUtilities;
        }

        public IResult<WorksOrder> AddWorksOrder(WorksOrderResource resource)
        {
            var employee = resource.Links.FirstOrDefault(l => l.Rel == "raised-by");

            if (employee == null)
            {
                return new BadRequestResult<WorksOrder>("Must supply an employee number when raising a works order");
            }

            resource.RaisedBy = employee.Href.ParseId();

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

            this.worksOrderUtilities.IssueSerialNumber(
                worksOrder.PartNumber,
                worksOrder.OrderNumber,
                worksOrder.DocType,
                worksOrder.RaisedBy,
                worksOrder.Quantity);

            this.transactionManager.Commit();

            return new CreatedResult<WorksOrder>(worksOrder);
        }

        public IResult<WorksOrder> UpdateWorksOrder(WorksOrderResource resource)
        {
            var employee = resource.Links?.FirstOrDefault(l => l.Rel == "updated-by");

            if (employee == null)
            {
                return new BadRequestResult<WorksOrder>("Must supply an employee number when updating a works order");
            }

            resource.CancelledBy = employee.Href.ParseId();

            var worksOrder = this.worksOrderRepository.FindById(resource.OrderNumber);

            if (worksOrder == null)
            {
                return new NotFoundResult<WorksOrder>();
            }

            try
            {
                worksOrder.UpdateWorksOrder(resource.Quantity, resource.BatchNotes,resource.CancelledBy, resource.ReasonCancelled);
            }
            catch (DomainException exception)
            {
                return new BadRequestResult<WorksOrder>(exception.Message);
            }
            this.transactionManager.Commit();

            return new SuccessResult<WorksOrder>(worksOrder);
        }

        public IResult<IEnumerable<WorksOrder>> SearchByBoardNumber(string boardNumber)
        {
            var result = this.worksOrderRepository.FilterBy(w => w.Part.IsBoardPart() && w.Part.PartNumber.Equals(boardNumber.ToUpper()));

            if (result.Count() > 1000)
            {
                result = result.Take(1000);
            }

            return new SuccessResult<IEnumerable<WorksOrder>>(result);
        }

        public IResult<WorksOrderPartDetails> GetWorksOrderPartDetails(string partNumber)
        {
            WorksOrderPartDetails worksOrderPartDetails;

            try
            {
                worksOrderPartDetails = this.worksOrderUtilities.GetWorksOrderDetails(partNumber);
            }
            catch (DomainException exception)
            {
                return new BadRequestResult<WorksOrderPartDetails>(exception.Message);
            }

            return new SuccessResult<WorksOrderPartDetails>(worksOrderPartDetails);
        }

        protected override WorksOrder CreateFromResource(WorksOrderResource resource)
        {
            return new WorksOrder
                       {
                           PartNumber = resource.PartNumber,
                           OrderNumber = resource.OrderNumber,
                           CancelledBy = resource.CancelledBy,
                           ReasonCancelled = resource.ReasonCancelled,
                           BatchNotes = resource.BatchNotes,
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

        protected override void UpdateFromResource(WorksOrder worksOrder, WorksOrderResource updateResource)
        {
            worksOrder.UpdateWorksOrder(
                updateResource.Quantity, 
                updateResource.BatchNotes, 
                updateResource.CancelledBy, 
                updateResource.ReasonCancelled);
        }

        protected override Expression<Func<WorksOrder, bool>> SearchExpression(string searchTerm)
        {
            return w => w.OrderNumber.ToString().Equals(searchTerm);
        }
    }
}
