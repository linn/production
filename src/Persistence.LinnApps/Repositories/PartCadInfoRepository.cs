namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class PartCadInfoRepository : IRepository<PartCadInfo, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartCadInfoRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartCadInfo FindById(int key)
        {
            return this.serviceDbContext.PartCadInfos.Where(m => m.MsId == key).ToList().FirstOrDefault();
        }

        public IQueryable<PartCadInfo> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(PartCadInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(PartCadInfo entity)
        {
            throw new NotImplementedException();
        }

        public PartCadInfo FindBy(Expression<Func<PartCadInfo, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartCadInfo> FilterBy(Expression<Func<PartCadInfo, bool>> expression)
        {
            return this.serviceDbContext.PartCadInfos.Where(expression).ToList().AsQueryable();
        }
    }
}