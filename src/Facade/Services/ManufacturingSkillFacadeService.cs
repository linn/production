namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingSkillFacadeService : FacadeFilterService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource, ManufacturingSkillResource>, IManufacturingSkillFacadeService
    {
        private readonly IRepository<ManufacturingSkill, string> manufacturingSkillRepository;

        public ManufacturingSkillFacadeService(
            IRepository<ManufacturingSkill, string> manufacturingSkillRepository, 
            ITransactionManager transactionManager) 
            : base(manufacturingSkillRepository, transactionManager)
        {
            this.manufacturingSkillRepository = manufacturingSkillRepository;
        }

        public IResult<IEnumerable<ManufacturingSkill>> GetValid()
        {
            return new SuccessResult<IEnumerable<ManufacturingSkill>>(
                this.manufacturingSkillRepository.FilterBy(c => !c.DateInvalid.HasValue));
        }

        protected override ManufacturingSkill CreateFromResource(ManufacturingSkillResource resource)
        {
            return new ManufacturingSkill
            {
                           SkillCode = resource.SkillCode,
                           Description = resource.Description,
                           HourlyRate = resource.HourlyRate,
                           DateInvalid = resource.DateInvalid != null
                                             ? DateTime.Parse(resource.DateInvalid)
                                             : (DateTime?)null
            };
        }

        protected override void UpdateFromResource(ManufacturingSkill entity, ManufacturingSkillResource updateResource)
        {
            entity.SkillCode = updateResource.SkillCode;
            entity.Description = updateResource.Description;
            entity.HourlyRate = updateResource.HourlyRate;
            entity.DateInvalid = updateResource.DateInvalid != null
                                     ? DateTime.Parse(updateResource.DateInvalid)
                                     : (DateTime?)null;
        }

        protected override Expression<Func<ManufacturingSkill, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ManufacturingSkill, bool>> FilterExpression(ManufacturingSkillResource searchTerms)
        {
            throw new NotImplementedException();
        }
    }
}
