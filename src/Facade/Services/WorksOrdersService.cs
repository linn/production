﻿namespace Linn.Production.Facade.Services
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
    using Linn.Production.Resources.RequestResources;

    public class WorksOrdersService : FacadeFilterService<WorksOrder, int, WorksOrderResource, WorksOrderResource, WorksOrderRequestResource>, IWorksOrdersService
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
            catch (DomainException exception)
            {
                return new BadRequestResult<WorksOrder>(exception.Message);
            }
        }

        public IResult<WorksOrder> UpdateWorksOrder(WorksOrderResource resource)
        {
            var employee = resource.Links?.FirstOrDefault(l => l.Rel == "updated-by");

            if (employee == null)
            {
                return new BadRequestResult<WorksOrder>("Must supply an employee number when updating a works order");
            }

            var worksOrder = this.worksOrderRepository.FindById(resource.OrderNumber);

            if (worksOrder == null)
            {
                return new NotFoundResult<WorksOrder>();
            }

            try
            {
                worksOrder.UpdateWorksOrder(resource.Quantity, resource.BatchNotes, resource.CancelledBy, resource.ReasonCancelled);
            }
            catch (DomainException exception)
            {
                return new BadRequestResult<WorksOrder>(exception.Message);
            }

            this.transactionManager.Commit();

            return new SuccessResult<WorksOrder>(worksOrder);
        }

        public IResult<IEnumerable<WorksOrder>> SearchByBoardNumber(
            string boardNumber,
            int? limit,
            string orderByDesc)
        {
            var result = this.worksOrderRepository.FilterBy(
                w => w.Part.IsBoardPart() && w.Part.PartNumber.Contains(boardNumber.ToUpper()));
            if (orderByDesc != null)
            {
                result = result.OrderByDescending(
                    w => w.GetType()
                        .GetProperty(
                            char.ToUpperInvariant(orderByDesc[0]) 
                            + orderByDesc.Substring(1)).GetValue(w, null)); 
            }

            if (limit.HasValue)
            {
                result = result.Take(limit.Value);
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
                           SaveBatchNotes = "Y",
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

        protected override Expression<Func<WorksOrder, bool>> FilterExpression(WorksOrderRequestResource searchResource)
        {
            return w =>
                (string.IsNullOrWhiteSpace(searchResource.SearchTerm)
                 || w.OrderNumber.ToString().Equals(searchResource.SearchTerm))
                && (string.IsNullOrWhiteSpace(searchResource.FromDate)
                    || w.DateRaised >= DateTime.Parse(searchResource.FromDate))
                && (string.IsNullOrWhiteSpace(searchResource.ToDate)
                    || w.DateRaised <= DateTime.Parse(searchResource.ToDate))
                && (string.IsNullOrWhiteSpace(searchResource.PartNumber)
                    || w.PartNumber.ToString().ToUpper().Equals(searchResource.PartNumber.ToUpper()));
        }
    }
}
