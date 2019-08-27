namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;

    public class BoardFailTypeRepository : IRepository<BoardFailType, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BoardFailTypeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public BoardFailType FindById(int key)
        {
            return this.serviceDbContext.BoardFailTypes.Where(t => t.Type == key).ToList().FirstOrDefault();
        }

        public IQueryable<BoardFailType> FindAll()
        {
            return this.serviceDbContext.BoardFailTypes;
        }

        public void Add(BoardFailType entity)
        {
            this.serviceDbContext.BoardFailTypes.Add(entity);
        }

        public void Remove(BoardFailType entity)
        {
            this.serviceDbContext.BoardFailTypes.Remove(entity);
        }

        public BoardFailType FindBy(Expression<Func<BoardFailType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BoardFailType> FilterBy(Expression<Func<BoardFailType, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}