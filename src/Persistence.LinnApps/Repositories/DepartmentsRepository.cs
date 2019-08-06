namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class DepartmentsRepository : IRepository<Department, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public DepartmentsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Department FindById(string key)
        {
            return this.serviceDbContext.Departments.Where(d => d.DepartmentCode == key).ToList().SingleOrDefault();
        }

        public IQueryable<Department> FindAll()
        {
           return this.serviceDbContext.Departments;
        }

        public void Add(Department entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Department entity)
        {
            throw new NotImplementedException();
        }

        public Department FindBy(Expression<Func<Department, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Department> FilterBy(Expression<Func<Department, bool>> expression)
        {
            return this.serviceDbContext.Departments.Where(expression);
        }
    }
}