using System;
using System.Linq;
using System.Linq.Expressions;
using Linn.Common.Persistence;
using Linn.Production.Domain.LinnApps;

namespace Linn.Production.Persistence.LinnApps.Repositories
{
    public class ManufacturingSkillsRepository : IRepository<ManufacturingSkill, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ManufacturingSkillsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ManufacturingSkill FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ManufacturingSkill> FindAll()
        {
            return this.serviceDbContext.ManufacturingSkills;
        }

        public void Add(ManufacturingSkill entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ManufacturingSkill entity)
        {
            throw new NotImplementedException();
        }

        public ManufacturingSkill FindBy(Expression<Func<ManufacturingSkill, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ManufacturingSkill> FilterBy(Expression<Func<ManufacturingSkill, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}