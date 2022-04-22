namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Triggers;

    public class PtlSettingsRepository : ISingleRecordRepository<PtlSettings>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PtlSettingsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PtlSettings GetRecord()
        {
            return this.serviceDbContext.PtlSettings.ToList().FirstOrDefault();
        }
    }
}
