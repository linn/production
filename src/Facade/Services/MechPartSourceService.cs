namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class MechPartSourceService : FacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource>
    {
        public MechPartSourceService(IRepository<MechPartSource, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override MechPartSource CreateFromResource(MechPartSourceResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(MechPartSource entity, MechPartSourceResource updateResource)
        {
            entity.LibraryRef = updateResource.LibraryRef;
            entity.FootprintRef = updateResource.FootprintRef;
        }

        protected override Expression<Func<MechPartSource, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}