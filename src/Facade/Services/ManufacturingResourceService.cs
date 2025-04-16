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
            return new ManufacturingResource
            {
                ResourceCode = resource.ResourceCode,
                Description = resource.Description,
                Cost = resource.Cost,
                DateInvalid = resource.DateInvalid != null
                                  ? DateTime.Parse(resource.DateInvalid)
                                  : (DateTime?)null
            };
        }

        protected override void UpdateFromResource(ManufacturingResource manufacturingResource, ManufacturingResourceResource updateResource)
        {
            manufacturingResource.Description = updateResource.Description;
            manufacturingResource.Cost = updateResource.Cost;
            manufacturingResource.DateInvalid = updateResource.DateInvalid != null
                                                    ? DateTime.Parse(updateResource.DateInvalid)
                                                    : (DateTime?)null;
        }

        protected override Expression<Func<ManufacturingResource, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
