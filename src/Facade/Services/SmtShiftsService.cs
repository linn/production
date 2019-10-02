namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class SmtShiftsService : FacadeService<SmtShift, string, SmtShiftResource, SmtShiftResource>
    {
        public SmtShiftsService(IRepository<SmtShift, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override SmtShift CreateFromResource(SmtShiftResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(SmtShift entity, SmtShiftResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<SmtShift, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}