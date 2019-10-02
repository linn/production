﻿namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;

    public class PartFailFaultCodeRepository : IRepository<PartFailFaultCode, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartFailFaultCodeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartFailFaultCode FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartFailFaultCode> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(PartFailFaultCode entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(PartFailFaultCode entity)
        {
            throw new NotImplementedException();
        }

        public PartFailFaultCode FindBy(Expression<Func<PartFailFaultCode, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartFailFaultCode> FilterBy(Expression<Func<PartFailFaultCode, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}