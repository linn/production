namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class EmployeeService : FacadeService<Employee, int, EmployeeResource, EmployeeResource>
    {
        public EmployeeService(IRepository<Employee, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Employee CreateFromResource(EmployeeResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Employee entity, EmployeeResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Employee, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}