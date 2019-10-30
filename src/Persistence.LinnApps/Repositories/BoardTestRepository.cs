namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BoardTests;

    using Microsoft.EntityFrameworkCore;

    public class BoardTestRepository : IRepository<BoardTest, BoardTestKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BoardTestRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public BoardTest FindById(BoardTestKey key)
        {
            return this.serviceDbContext.BoardTests
                .Where(t => t.BoardSerialNumber == key.BoardSerialNumber && t.Seq == key.Seq)
                .ToList().FirstOrDefault();
        }

        public IQueryable<BoardTest> FindAll()
        {
            return this.serviceDbContext.BoardTests;
        }

        public void Add(BoardTest entity)
        {
            this.serviceDbContext.BoardTests.Add(entity);
        }

        public void Remove(BoardTest entity)
        {
            throw new NotImplementedException();
        }

        public BoardTest FindBy(Expression<Func<BoardTest, bool>> expression)
        {
            return this.serviceDbContext.BoardTests
                .Include(b => b.FailType)
                .Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<BoardTest> FilterBy(Expression<Func<BoardTest, bool>> expression)
        {
            return this.serviceDbContext.BoardTests
                .Include(b => b.FailType)
                .Where(expression);
        }
    }
}