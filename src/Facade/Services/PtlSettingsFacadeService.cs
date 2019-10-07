namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Resources;

    public class PtlSettingsFacadeService : SingleRecordFacadeService<PtlSettings, PtlSettingsResource>
    {
        public PtlSettingsFacadeService(ISingleRecordRepository<PtlSettings> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override void UpdateFromResource(PtlSettings entity, PtlSettingsResource updateResource)
        {
            entity.BuildToMonthEndFromDays = updateResource.BuildToMonthEndFromDays;
            entity.DaysToLookAhead = updateResource.DaysToLookAhead;
            entity.FinalAssemblyDaysToLookAhead = updateResource.FinalAssemblyDaysToLookAhead;
            entity.PriorityCutOffDays = updateResource.PriorityCutOffDays;
            entity.PriorityStrategy = updateResource.PriorityStrategy;
            entity.SubAssemblyDaysToLookAhead = updateResource.SubAssemblyDaysToLookAhead;
        }
    }
}