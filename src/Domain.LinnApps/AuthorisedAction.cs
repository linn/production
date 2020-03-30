namespace Linn.Production.Domain.LinnApps
{
    public class AuthorisedAction
    {
        public const string PtlSettingsUpdate = "production-trigger-levels.admin";

        public const string ProductionTriggerLevelUpdate = "production-trigger-levels-ut.admin";

        public const string StartTriggerRun = "production-trigger-levels.admin";

        public const string ManufacturingRouteUpdate = "manufacturing-routes.admin";

        public const string SerialNumberReissueRebuild = "serial-number.reissue";

        public const string BuildPlanAdd = "build-plans.admin";

        public const string BuildPlanUpdate = "build-plans.admin";

        public const string BuildPlanDetailAdd = "build-plans.admin";

        public const string BuildPlanDetailUpdate = "build-plans.admin";

        public const string ManufacturingTimings = "manufacturing-timings.admin";

        // TODO remove this
        public const string PartCadInfoUpdate = "part-cad-info.admin";

        public const string PartUpdate = "part.admin";
    }
}
