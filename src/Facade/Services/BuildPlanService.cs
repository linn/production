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
            return new BuildPlan()
                       {
                           BuildPlanName = resource.BuildPlanName,
                           Description = resource.Description,
                           DateCreated = DateTime.Parse(resource.DateCreated),
                           DateInvalid =
                               resource.DateInvalid != null ? DateTime.Parse(resource.DateInvalid) : (DateTime?)null,
                           LastMrpJobRef = resource.LastMrpJobRef,
                           LastMrpDateStarted =
                               resource.LastMrpDateStarted != null
                                   ? DateTime.Parse(resource.LastMrpDateStarted)
                                   : (DateTime?)null,
                           LastMrpDateFinished = resource.LastMrpDateFinished != null
                                                     ? DateTime.Parse(resource.LastMrpDateFinished)
                                                     : (DateTime?)null
                       };
        }

        protected override void UpdateFromResource(BuildPlan buildPlan, BuildPlanResource resource)
        {
            buildPlan.Description = resource.Description;
            buildPlan.DateInvalid =
                resource.DateInvalid != null ? DateTime.Parse(resource.DateInvalid) : (DateTime?)null;
            buildPlan.LastMrpJobRef = resource.LastMrpJobRef;
            buildPlan.LastMrpDateStarted = resource.LastMrpDateStarted != null
                                               ? DateTime.Parse(resource.LastMrpDateStarted)
                                               : (DateTime?)null;
            buildPlan.LastMrpDateFinished = resource.LastMrpDateFinished != null
                                                ? DateTime.Parse(resource.LastMrpDateFinished)
                                                : (DateTime?)null;
        }

        protected override Expression<Func<BuildPlan, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}