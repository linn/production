namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrderLabelsService : FacadeService<WorksOrderLabel, WorksOrderLabelKey, WorksOrderLabelResource, WorksOrderLabelResource>
    {
        public WorksOrderLabelsService(IRepository<WorksOrderLabel, WorksOrderLabelKey> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override WorksOrderLabel CreateFromResource(WorksOrderLabelResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(WorksOrderLabel entity, WorksOrderLabelResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<WorksOrderLabel, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}