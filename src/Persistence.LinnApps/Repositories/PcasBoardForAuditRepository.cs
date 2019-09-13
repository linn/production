namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.PCAS;

    public class PcasBoardForAuditRepository : IRepository<PcasBoardForAudit, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PcasBoardForAuditRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PcasBoardForAudit FindById(string key)
        {
            return this.serviceDbContext.PcasBoardsForAudit.Where(p => p.BoardCode == key).ToList().FirstOrDefault();
        }

        public IQueryable<PcasBoardForAudit> FindAll()
        {
            return this.serviceDbContext.PcasBoardsForAudit;
        }

        public void Add(PcasBoardForAudit entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(PcasBoardForAudit entity)
        {
            throw new NotImplementedException();
        }

        public PcasBoardForAudit FindBy(Expression<Func<PcasBoardForAudit, bool>> expression)
        {
            return this.serviceDbContext.PcasBoardsForAudit.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<PcasBoardForAudit> FilterBy(Expression<Func<PcasBoardForAudit, bool>> expression)
        {
            return this.serviceDbContext.PcasBoardsForAudit.Where(expression);
        }
    }
}