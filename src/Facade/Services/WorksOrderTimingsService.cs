namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrderTimingsService : FacadeService<WorksOrderTiming, int, WorksOrderTimingResource, WorksOrderTimingResource>, IWorksOrderTimingsService
    {
        private readonly IRepository<WorksOrderTiming, int> worksOrderTimingRepository;

        private readonly ITransactionManager transactionManager;

        public WorksOrderTimingsService(
            IRepository<WorksOrderTiming, int> worksOrderTimingRepository,
            ITransactionManager transactionManager)
            : base(worksOrderTimingRepository, transactionManager)
        {
            this.worksOrderTimingRepository = worksOrderTimingRepository;
            this.transactionManager = transactionManager;
        }

        public IResult<IEnumerable<WorksOrderTiming>> SearchByDates(DateTime start, DateTime end)
        {
            var result = this.worksOrderTimingRepository.FilterBy(
                w => start < w.StartTime && w.StartTime < end);
            //this was what was in the sql, but should maybe have or end date is between?
            return new SuccessResult<IEnumerable<WorksOrderTiming>>(result);
        }

        protected override WorksOrderTiming CreateFromResource(WorksOrderTimingResource resource)
        {
            return new WorksOrderTiming
            {
                OrderNumber = resource.OrderNumber,
                OperationNumber = resource.OperationNumber,
                OperationType = resource.OperationType,
                BuiltBy = resource.BuiltBy,
                ResourceCode = resource.ResourceCode,
                RouteCode = resource.RouteCode,
                StartTime = resource.StartTime,
                EndTime = resource.EndTime,
                TimeTaken = resource.TimeTaken
            };
        }

        protected override void UpdateFromResource(WorksOrderTiming worksOrderTiming, WorksOrderTimingResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<WorksOrderTiming, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
