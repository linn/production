using System;
using System.Linq;
using System.Linq.Expressions;
using Linn.Common.Persistence;
using Linn.Production.Domain.LinnApps.SerialNumberReissue;

namespace Linn.Production.Persistence.LinnApps.Repositories
{
    public class SerialNumberReissueRepository : IRepository<SerialNumberReissue, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SerialNumberReissueRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SerialNumberReissue FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SerialNumberReissue> FindAll()
        {
            throw new NotImplementedException();
        }

        // TODO get the id here
        public void Add(SerialNumberReissue entity)
        {
            this.serviceDbContext.SerialNumberReissues.Add(entity);
        }

        public void Remove(SerialNumberReissue entity)
        {
            throw new NotImplementedException();
        }

        public SerialNumberReissue FindBy(Expression<Func<SerialNumberReissue, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SerialNumberReissue> FilterBy(Expression<Func<SerialNumberReissue, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
