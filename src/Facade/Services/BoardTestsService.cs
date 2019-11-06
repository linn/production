namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BoardTests;
    using Linn.Production.Resources;

    public class BoardTestsService : FacadeService<BoardTest, BoardTestKey, BoardTestResource, BoardTestResource>
    {
        public BoardTestsService(IRepository<BoardTest, BoardTestKey> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override BoardTest CreateFromResource(BoardTestResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(BoardTest boardTest, BoardTestResource resource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<BoardTest, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}