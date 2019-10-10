namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingRouteService : FacadeService<ManufacturingRoute, string, ManufacturingRouteResource, ManufacturingRouteResource>
    {
        public ManufacturingRouteService(
            IRepository<ManufacturingRoute, string> repository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
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
        }

        protected override Expression<Func<ManufacturingRoute, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
