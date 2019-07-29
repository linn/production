using System;
using System.Linq;
using System.Linq.Expressions;
using Linn.Common.Persistence;
using Linn.Production.Domain.LinnApps.SerialNumberIssue;

namespace Linn.Production.Persistence.LinnApps.Repositories
{
    public class SerialNumberIssueRepository : IRepository<SerialNumberIssue, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SerialNumberIssueRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SerialNumberIssue FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SerialNumberIssue> FindAll()
        {
            throw new NotImplementedException();
        }

        // TODO get the id here
        public void Add(SerialNumberIssue entity)
        {
            this.serviceDbContext.SerialNumberIssues.Add(entity);
        }

        public void Remove(SerialNumberIssue entity)
        {
            throw new NotImplementedException();
        }

        public SerialNumberIssue FindBy(Expression<Func<SerialNumberIssue, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SerialNumberIssue> FilterBy(Expression<Func<SerialNumberIssue, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
