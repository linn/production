namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class BoardFailTypesService : FacadeService<BoardFailType, int, BoardFailTypeResource, BoardFailTypeResource>
    {
        public BoardFailTypesService(IRepository<BoardFailType, int> repository, ITransactionManager transactionManager) : base(repository, transactionManager)
        {
        }

        protected override BoardFailType CreateFromResource(BoardFailTypeResource resource)
        {
            return new BoardFailType
            {
                Type = resource.FailType,
                Description = resource.Description,
            };
        }

        protected override void UpdateFromResource(BoardFailType boardFailType, BoardFailTypeResource resource)
        {
            boardFailType.Description = resource.Description;
        }

        protected override Expression<Func<BoardFailType, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}