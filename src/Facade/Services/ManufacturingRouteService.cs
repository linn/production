namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingRouteService : FacadeService<ManufacturingRoute, string, ManufacturingRouteResource,
        ManufacturingRouteResource>
    {
        private readonly IFacadeService<ManufacturingOperation, int, ManufacturingOperationResource, ManufacturingOperationResource> manufacturingOperationService;

        public ManufacturingRouteService(
            IRepository<ManufacturingRoute, string> repository,
            ITransactionManager transactionManager,
            IFacadeService<ManufacturingOperation, int, ManufacturingOperationResource, ManufacturingOperationResource> manufacturingOperationService)
            : base(repository, transactionManager)
        {
            this.manufacturingOperationService = manufacturingOperationService;
        }

        protected override ManufacturingRoute CreateFromResource(ManufacturingRouteResource resource)
        {
            return new ManufacturingRoute(resource.RouteCode, resource.Description, resource.Notes);
        }

        protected override void UpdateFromResource(ManufacturingRoute entity, ManufacturingRouteResource updateResource)
        {
            entity.RouteCode = updateResource.RouteCode;
            entity.Description = updateResource.Description;
            entity.Notes = updateResource.Notes;

            foreach (var operation in updateResource.Operations)
            {
              if (operation.ManufacturingId > 0)
              {
                  this.manufacturingOperationService.Update(operation.ManufacturingId, operation);
              }
              else
              {
                  this.manufacturingOperationService.Add(operation);
              }
            }
        }

        protected override Expression<Func<ManufacturingRoute, bool>> SearchExpression(string searchTerm)
        {
            return w => w.RouteCode.ToUpper().Contains(searchTerm.ToUpper());
        }
    }
}
