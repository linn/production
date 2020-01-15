﻿namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class PartFailLogRepository : IQueryRepository<PartFailLog>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartFailLogRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartFailLog FindBy(Expression<Func<PartFailLog, bool>> expression)
        {
            return this.serviceDbContext.PartFailLogs.Include(p => p.Part).Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<PartFailLog> FilterBy(Expression<Func<PartFailLog, bool>> expression)
        {
            return this.serviceDbContext.PartFailLogs.Include(p => p.Part).Where(expression);
        }

        public IQueryable<PartFailLog> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}