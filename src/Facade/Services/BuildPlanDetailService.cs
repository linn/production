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
        private IBuildPlanDetailFactory buildPlanDetailFactory;

        public BuildPlanDetailService(IRepository<BuildPlanDetail, BuildPlanDetailKey> repository, ITransactionManager transactionManager, IBuildPlanDetailFactory buildPlanDetailFactory)
            : base(repository, transactionManager)
        {
            this.buildPlanDetailFactory = buildPlanDetailFactory;
        }

        protected override BuildPlanDetail CreateFromResource(BuildPlanDetailResource resource)
        {
            var buildPlanDetail = new BuildPlanDetail
                                      {
                                          BuildPlanName = resource.BuildPlanName,
                                          FromLinnWeekNumber = resource.FromLinnWeekNumber,
                                          PartNumber = resource.PartNumber,
                                          Quantity = resource.Quantity,
                                          RuleCode = resource.RuleCode,
                                          ToLinnWeekNumber = resource.ToLinnWeekNumber
                                      };

            buildPlanDetail.Validate();

            return buildPlanDetail;
        }

        protected override void UpdateFromResource(BuildPlanDetail entity, BuildPlanDetailResource updateResource)
        {
            entity.Quantity = updateResource.Quantity;
            entity.RuleCode = updateResource.RuleCode;
            entity.ToLinnWeekNumber = updateResource.ToLinnWeekNumber;

            entity.Validate();
        }

        protected override Expression<Func<BuildPlanDetail, bool>> SearchExpression(string searchTerm)
        {
            return b => b.BuildPlanName.Equals(searchTerm);
        }
    }
}