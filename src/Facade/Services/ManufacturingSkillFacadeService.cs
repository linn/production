﻿namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;

    public class ManufacturingSkillFacadeService : FacadeFilterService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource, IncludeInvalidRequestResource>
    {
        private readonly IRepository<ManufacturingSkill, string> manufacturingSkillRepository;

        public ManufacturingSkillFacadeService(
            IRepository<ManufacturingSkill, string> manufacturingSkillRepository, 
            ITransactionManager transactionManager) 
            : base(manufacturingSkillRepository, transactionManager)
        {
            this.manufacturingSkillRepository = manufacturingSkillRepository;
        }

        protected override ManufacturingSkill CreateFromResource(ManufacturingSkillResource resource)
        {
            return new ManufacturingSkill(
                resource.SkillCode,
                resource.Description,
                resource.HourlyRate);
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

        protected override Expression<Func<ManufacturingSkill, bool>> FilterExpression(IncludeInvalidRequestResource searchTerms)
        {
            return m => searchTerms.IncludeInvalid.GetValueOrDefault() || !m.DateInvalid.HasValue;
        }
    }
}
