namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanDetailService : FacadeService<BuildPlanDetail, BuildPlanDetailKey, BuildPlanDetailResource, BuildPlanDetailResource>
    {
        public BuildPlanDetailService(IRepository<BuildPlanDetail, BuildPlanDetailKey> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override BuildPlanDetail CreateFromResource(BuildPlanDetailResource resource)
        {
            return new BuildPlanDetail
                       {
                           BuildPlanName = resource.BuildPlanName,
                           FromLinnWeekNumber = resource.FromLinnWeekNumber,
                           PartNumber = resource.PartNumber,
                           Quantity = resource.Quantity,
                           RuleCode = resource.RuleCode,
                           ToLinnWeekNumber = resource.ToLinnWeekNumber
                       };
        }

        protected override void UpdateFromResource(BuildPlanDetail entity, BuildPlanDetailResource updateResource)
        {
            entity.BuildPlanName = updateResource.BuildPlanName;
            entity.PartNumber = updateResource.PartNumber;
            entity.FromLinnWeekNumber = updateResource.FromLinnWeekNumber;
            entity.Quantity = updateResource.Quantity;
            entity.RuleCode = updateResource.RuleCode;
            entity.ToLinnWeekNumber = updateResource.ToLinnWeekNumber;
        }

        protected override Expression<Func<BuildPlanDetail, bool>> SearchExpression(string searchTerm)
        {
            return b => b.BuildPlanName.Equals(searchTerm);
        }
    }
}