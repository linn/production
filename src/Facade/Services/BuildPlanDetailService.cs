namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Resources;

    public class BuildPlanDetailService : FacadeService<BuildPlanDetail, BuildPlanDetailKey, BuildPlanDetailResource, BuildPlanDetailResource>
    {
        private readonly ILinnWeekPack linnWeekPack;

        public BuildPlanDetailService(
            IRepository<BuildPlanDetail, BuildPlanDetailKey> repository,
            ITransactionManager transactionManager,
            ILinnWeekPack linnWeekPack)
            : base(repository, transactionManager)
        {
            this.linnWeekPack = linnWeekPack;
        }

        protected override BuildPlanDetail CreateFromResource(BuildPlanDetailResource resource)
        {
            var buildPlanDetail = new BuildPlanDetail
                                      {
                                          BuildPlanName = resource.BuildPlanName,
                                          FromLinnWeekNumber = this.linnWeekPack.LinnWeekNumber(resource.FromDate),
                                          PartNumber = resource.PartNumber,
                                          Quantity = resource.Quantity,
                                          RuleCode = resource.RuleCode,
                                          ToLinnWeekNumber = this.linnWeekPack.LinnWeekNumber(resource.ToDate)
                                      };

            buildPlanDetail.Validate();

            return buildPlanDetail;
        }

        protected override void UpdateFromResource(BuildPlanDetail entity, BuildPlanDetailResource updateResource)
        {
            entity.Quantity = updateResource.Quantity;
            entity.RuleCode = updateResource.RuleCode;
            entity.ToLinnWeekNumber = this.linnWeekPack.LinnWeekNumber(updateResource.ToDate);

            entity.Validate();
        }

        protected override Expression<Func<BuildPlanDetail, bool>> SearchExpression(string searchTerm)
        {
            return b => b.BuildPlanName.Equals(searchTerm);
        }
    }
}