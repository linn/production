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
        public ManufacturingSkillService(IRepository<ManufacturingSkill, string> repository, ITransactionManager transactionManager) : base(repository, transactionManager)
        {
        }

        protected override ManufacturingSkill CreateFromResource(ManufacturingSkillResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(ManufacturingSkill entity, ManufacturingSkillResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ManufacturingSkill, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}