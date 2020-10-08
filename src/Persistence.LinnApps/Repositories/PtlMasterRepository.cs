namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Triggers;

    public class PtlMasterRepository : ISingleRecordRepository<PtlMaster>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PtlMasterRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PtlMaster GetRecord()
        {
            return this.serviceDbContext.PtlMaster.ToList().FirstOrDefault();
        }

        public void UpdateRecord(PtlMaster newValues)
        {
            throw new System.NotImplementedException();
        }
    }
}