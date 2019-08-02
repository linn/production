using System;
using System.Linq.Expressions;
using Linn.Common.Facade;
using Linn.Common.Persistence;
using Linn.Production.Domain.LinnApps;
using Linn.Production.Resources;
using Remotion.Linq.Clauses;

namespace Linn.Production.Facade.Services
{
    public class DepartmentService : FacadeService<Department, string, DepartmentResource, DepartmentResource>
    {
        public DepartmentService(IRepository<Department, string> repository, ITransactionManager transactionManager) 
            : base(repository, transactionManager)
        {
        }

        protected override Department CreateFromResource(DepartmentResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Department entity, DepartmentResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Department, bool>> SearchExpression(string searchTerm)
        {
            return d => d.PersonnelDepartment == searchTerm;
        }
    }
}
