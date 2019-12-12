namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class AteTestService : FacadeService<AteTest, int, AteTestResource, AteTestResource>
    {
        public AteTestService(IRepository<AteTest, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override AteTest CreateFromResource(AteTestResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(AteTest entity, AteTestResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<AteTest, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}