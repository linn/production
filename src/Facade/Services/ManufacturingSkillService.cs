namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using System;
    using System.Linq.Expressions;

    public class ManufacturingSkillService : FacadeService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource>
    {
        public ManufacturingSkillService(IRepository<ManufacturingSkill, string> repository, ITransactionManager transactionManager) : base(repository, transactionManager)
        {
        }

        protected override ManufacturingSkill CreateFromResource(ManufacturingSkillResource resource)
        {
            return new ManufacturingSkill(resource.SkillCode, resource.Description, resource.HourlyRate);
        }

        protected override void UpdateFromResource(ManufacturingSkill entity, ManufacturingSkillResource updateResource)
        {
            entity.SkillCode = updateResource.SkillCode;
            entity.Description = updateResource.Description;
            entity.HourlyRate = updateResource.HourlyRate;
        }

        protected override Expression<Func<ManufacturingSkill, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
