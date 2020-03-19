﻿namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class MechPartSourceRepository : IRepository<MechPartSource, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public MechPartSourceRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public MechPartSource FindById(int key)
        {
            return this.serviceDbContext.MechPartSources.Where(m => m.MsId == key).ToList().FirstOrDefault();
        }

        public IQueryable<MechPartSource> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(MechPartSource entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(MechPartSource entity)
        {
            throw new NotImplementedException();
        }

        public MechPartSource FindBy(Expression<Func<MechPartSource, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MechPartSource> FilterBy(Expression<Func<MechPartSource, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}