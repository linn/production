namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using Linn.Production.Domain.LinnApps.Measures;

    public class OsrRunMasterRepository : ISingleRecordRepository<OsrRunMaster>
    {
        private readonly ServiceDbContext serviceDbContext;

        public OsrRunMasterRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public OsrRunMaster GetRecord()
        {
            return this.serviceDbContext.OsrRunMaster;
        }

        public void UpdateRecord(OsrRunMaster newValues)
        {
            throw new System.NotImplementedException();
        }
    }
}