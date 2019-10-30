namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class EmployeeDepartmentViewRepository : IQueryRepository<EmployeeDepartmentView>
    {
        private readonly ServiceDbContext serviceDbContext;

        public EmployeeDepartmentViewRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public EmployeeDepartmentView FindBy(Expression<Func<EmployeeDepartmentView, bool>> expression)
        {
            return this.serviceDbContext.EmployeeDepartmentView.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<EmployeeDepartmentView> FilterBy(Expression<Func<EmployeeDepartmentView, bool>> expression)
        {
            return this.serviceDbContext.EmployeeDepartmentView.Where(expression);
        }

        public IQueryable<EmployeeDepartmentView> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}