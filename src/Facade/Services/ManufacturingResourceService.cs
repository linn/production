namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingResourceService : FacadeService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource>
    {
        public ManufacturingResourceService(IRepository<ManufacturingResource, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override ManufacturingResource CreateFromResource(ManufacturingResourceResource resource)
        {
            return new ManufacturingResource(resource.ResourceCode, resource.Description, resource.Cost);
        }

        protected override void UpdateFromResource(ManufacturingResource manufacturingResource, ManufacturingResourceResource updateResource)
        {
            manufacturingResource.Description = updateResource.Description;
            manufacturingResource.Cost = updateResource.Cost;
        }

        protected override Expression<Func<ManufacturingResource, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
