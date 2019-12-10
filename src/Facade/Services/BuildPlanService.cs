namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanService : FacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource>
    {
        public BuildPlanService(IRepository<BuildPlan, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override BuildPlan CreateFromResource(BuildPlanResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(BuildPlan entity, BuildPlanResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<BuildPlan, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}