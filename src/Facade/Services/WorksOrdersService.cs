namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrdersService : FacadeService<WorksOrder, int, WorksOrderResource, WorksOrderResource>
    {
        public WorksOrdersService(IRepository<WorksOrder, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override WorksOrder CreateFromResource(WorksOrderResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(WorksOrder entity, WorksOrderResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<WorksOrder, bool>> SearchExpression(string searchTerm)
        {
            return w => w.OrderNumber.ToString().Contains(searchTerm);
        }
    }
}