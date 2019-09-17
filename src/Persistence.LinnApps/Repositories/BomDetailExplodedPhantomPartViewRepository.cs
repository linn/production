namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Persistence.LinnApps;

    public class BomDetailExplodedPhantomPartViewRepository : IRepository<BomDetailExplodedPhantomPartView, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BomDetailExplodedPhantomPartViewRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public BomDetailExplodedPhantomPartView FindById(int key)
        {
            return this.serviceDbContext.BomDetailExplodedPhantomPartView
                .Where(f => f.BomId == key).ToList().FirstOrDefault();
        }

        public IQueryable<BomDetailExplodedPhantomPartView> FindAll()
        {
            return this.serviceDbContext.BomDetailExplodedPhantomPartView;
        }

        public void Add(BomDetailExplodedPhantomPartView entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(BomDetailExplodedPhantomPartView entity)
        {
            throw new NotImplementedException();
        }

        public BomDetailExplodedPhantomPartView FindBy(Expression<Func<BomDetailExplodedPhantomPartView, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BomDetailExplodedPhantomPartView> FilterBy(Expression<Func<BomDetailExplodedPhantomPartView, bool>> expression)
        {
            return this.serviceDbContext.BomDetailExplodedPhantomPartView.Where(expression);
        }
    }
}