namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System.Linq;

    using Linn.Production.Domain.LinnApps.Triggers;

    public class PtlSettingsRepository : IMasterRepository<PtlSettings>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PtlSettingsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PtlSettings GetMasterRecord()
        {
            return this.serviceDbContext.PtlSettings.ToList().FirstOrDefault();
        }

        public void UpdateRecord(PtlSettings newValues)
        {
            throw new System.NotImplementedException();
        }
    }
}