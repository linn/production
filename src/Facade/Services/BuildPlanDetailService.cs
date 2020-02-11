namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Resources;

    public class BuildPlanDetailService :
        FacadeService<BuildPlanDetail, BuildPlanDetailKey, BuildPlanDetailResource, BuildPlanDetailResource>,
        IBuildPlanDetailsService
    {
        private readonly ILinnWeekPack linnWeekPack;

        private readonly IRepository<BuildPlanDetail, BuildPlanDetailKey> repository;

        private readonly ITransactionManager transactionManager;

        public BuildPlanDetailService(
            IRepository<BuildPlanDetail, BuildPlanDetailKey> repository,
            ITransactionManager transactionManager,
            ILinnWeekPack linnWeekPack)
            : base(repository, transactionManager)
        {
            this.repository = repository;
            this.transactionManager = transactionManager;
            this.linnWeekPack = linnWeekPack;
        }

        public IResult<ResponseModel<BuildPlanDetail>> UpdateBuildPlanDetail(BuildPlanDetailResource resource, IEnumerable<string> privileges)
        {
            var fromWeek = this.linnWeekPack.LinnWeekNumber(DateTime.Parse(resource.FromDate));

            var key = new BuildPlanDetailKey
                          {
                              PartNumber = resource.PartNumber,
                              BuildPlanName = resource.BuildPlanName,
                              FromLinnWeekNumber = fromWeek
                          };

            var buildPlanDetail = this.repository.FindById(key);

            if (buildPlanDetail == null)
            {
                return new NotFoundResult<ResponseModel<BuildPlanDetail>>();
            }

            try
            {
                this.UpdateFromResource(buildPlanDetail, resource);
            }
            catch (DomainException exception)
            {
                return new BadRequestResult<ResponseModel<BuildPlanDetail>>($"Error updating - {exception.Message}");
            }

            this.transactionManager.Commit();

            return new SuccessResult<ResponseModel<BuildPlanDetail>>(
                new ResponseModel<BuildPlanDetail>(buildPlanDetail, privileges));
        }

        protected override BuildPlanDetail CreateFromResource(BuildPlanDetailResource resource)
        {
            var buildPlanDetail = new BuildPlanDetail
                                      {
                                          BuildPlanName = resource.BuildPlanName,
                                          FromLinnWeekNumber = this.linnWeekPack.LinnWeekNumber(DateTime.Parse(resource.FromDate)),
                                          PartNumber = resource.PartNumber,
                                          Quantity = resource.Quantity,
                                          RuleCode = resource.RuleCode,
                                          ToLinnWeekNumber = this.LinnWeekNumber(resource.ToDate)
                                      };

            buildPlanDetail.Validate();

            return buildPlanDetail;
        }

        protected override void UpdateFromResource(BuildPlanDetail entity, BuildPlanDetailResource updateResource)
        {
            entity.Quantity = updateResource.Quantity;
            entity.RuleCode = updateResource.RuleCode;
            entity.ToLinnWeekNumber = this.LinnWeekNumber(updateResource.ToDate);

            entity.Validate();
        }

        protected override Expression<Func<BuildPlanDetail, bool>> SearchExpression(string searchTerm)
        {
            return b => b.BuildPlanName.Equals(searchTerm);
        }

        private int LinnWeekNumber(string date)
        {
            return date == null ? -999 : this.linnWeekPack.LinnWeekNumber(DateTime.Parse(date));
        }
    }
}