﻿namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Triggers;

    public class ProductionTriggerQueryRepository : IQueryRepository<ProductionTrigger>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductionTriggerQueryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProductionTrigger FindBy(Expression<Func<ProductionTrigger, bool>> expression)
        {
            return this.serviceDbContext.ProductionTriggers.SingleOrDefault(expression);
        }

        public IQueryable<ProductionTrigger> FilterBy(Expression<Func<ProductionTrigger, bool>> expression)
        {
            return this.serviceDbContext.ProductionTriggers.Where(expression);
        }

        public IQueryable<ProductionTrigger> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}