namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PartCadInfoService : FacadeService<PartCadInfo, int, PartCadInfoResource, PartCadInfoResource>
    {
        public PartCadInfoService(IRepository<PartCadInfo, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PartCadInfo CreateFromResource(PartCadInfoResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PartCadInfo entity, PartCadInfoResource updateResource)
        {
            entity.LibraryRef = updateResource.LibraryRef;
            entity.FootprintRef = updateResource.FootprintRef;
            entity.LibraryName = updateResource.LibraryName;
        }

        protected override Expression<Func<PartCadInfo, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}