namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using Linn.Production.Domain.LinnApps.Triggers;

    public class PtlMasterRepository : IMasterRepository<PtlMaster>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PtlMasterRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PtlMaster GetMasterRecord()
        {
            return this.serviceDbContext.PtlMaster;
        }

        public void UpdateRecord(PtlMaster newValues)
        {
            throw new System.NotImplementedException();
        }
    }
}