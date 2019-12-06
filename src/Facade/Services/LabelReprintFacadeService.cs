namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Extensions;
    using Linn.Production.Resources;

    public class LabelReprintFacadeService : FacadeService<LabelReprint, int, LabelReprintResource, LabelReprintResource>
    {
        private readonly ILabelService labelService;

        public LabelReprintFacadeService(
            IRepository<LabelReprint, int> repository,
            ITransactionManager transactionManager,
            ILabelService labelService)
            : base(repository, transactionManager)
        {
            this.labelService = labelService;
        }

        protected override LabelReprint CreateFromResource(LabelReprintResource resource)
        {
            return this.labelService.CreateLabelReprint(
                resource.Links.First(a => a.Rel == "requested-by").Href.ParseId(),
                resource.Reason,
                resource.PartNumber,
                resource.SerialNumber,
                resource.WorksOrderNumber,
                resource.LabelTypeCode,
                resource.NumberOfProducts,
                resource.ReprintType,
                resource.NewPartNumber);
        }

        protected override void UpdateFromResource(LabelReprint entity, LabelReprintResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<LabelReprint, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
