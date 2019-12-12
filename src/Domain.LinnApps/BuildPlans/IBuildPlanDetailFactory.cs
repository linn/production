namespace Linn.Production.Domain.LinnApps.BuildPlans
{
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public interface IBuildPlanDetailFactory
    {
        // TODO change class date times to ints and raise through this factory
        // which will have the linn week pack
        BuildPlanDetail CreateBuildPlanDetail(BuildPlanDetail buildPlanDetailToBeCreated);

        BuildPlanDetail UpdateBuildPlanDetail(int? quantity, string ruleCode, int? toLinnWeekNumber);
    }

    public class BuildPlanDetailFactory : IBuildPlanDetailFactory
    {
        private ILinnWeekPack linnWeekPack;

        public BuildPlanDetailFactory(ILinnWeekPack linnWeekPack)
        {
            this.linnWeekPack = linnWeekPack;
        }

        public BuildPlanDetail CreateBuildPlanDetail(BuildPlanDetail buildPlanDetailToBeCreated)
        {
            throw new System.NotImplementedException();
        }

        public BuildPlanDetail UpdateBuildPlanDetail(int? quantity, string ruleCode, int? toLinnWeekNumber)
        {
            throw new System.NotImplementedException();
        }
    }
}
