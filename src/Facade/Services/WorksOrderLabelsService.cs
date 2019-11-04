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
            return new WorksOrderLabel
                       {
                           PartNumber = resource.PartNumber,
                           Sequence = resource.Sequence,
                           LabelText = resource.LabelText
                       };
        }

        protected override void UpdateFromResource(WorksOrderLabel entity, WorksOrderLabelResource updateResource)
        {
            entity.LabelText = updateResource.LabelText;
        }

        protected override Expression<Func<WorksOrderLabel, bool>> SearchExpression(string searchTerm)
        {
            return l => l.PartNumber.Contains(searchTerm.ToUpper());
        }
    }
}