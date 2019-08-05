using System;
using System.Linq.Expressions;
using Linn.Common.Facade;
using Linn.Common.Persistence;
using Linn.Production.Domain.LinnApps;
using Linn.Production.Resources;

namespace Linn.Production.Facade.Services
{
    public class ManufacturingSkillService : FacadeService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource>
    {
       // private readonly IRepository<ManufacturingSkill, int> manufacturingSkillRepository;

        public ManufacturingSkillService(IRepository<ManufacturingSkill, string> repository, ITransactionManager transactionManager) : base(repository, transactionManager)
           // , IRepository<ManufacturingSkill, int> manufacturingSkillRepository) 
        {
           // this.manufacturingSkillRepository = manufacturingSkillRepository;
        }

        public IResult<ManufacturingSkill> CreateManufacturingSkill(ManufacturingSkillResource resource)
        {
            if (resource.SkillCode == null || resource.Description == null || resource.HourlyRate == null)
            {
                return new BadRequestResult<ManufacturingSkill>();
            }

            return this.Add(resource);
        }

        public IResult<ManufacturingSkill> UpdateManufacturingSkill(ManufacturingSkillResource resource)
        {
            if (resource.SkillCode == null || resource.Description == null || resource.HourlyRate == null)
            {
                return new BadRequestResult<ManufacturingSkill>();
            }

            //var skill = this.manufacturingSkillRepository.FindBy(x => x.SkillCode == resource.SkillCode);
            
            return this.Update(resource.SkillCode, resource);
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