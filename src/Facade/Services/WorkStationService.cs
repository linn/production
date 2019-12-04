namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class WorkStationService : FacadeService<WorkStation, string, WorkStationResource, WorkStationResource>
    {
        private readonly IRepository<WorkStation, string> workStationRepository;

        public WorkStationService(
            IRepository<WorkStation, string> workStationRepository,
            ITransactionManager transactionManager)
            : base(workStationRepository, transactionManager)
        {
            this.workStationRepository = workStationRepository;
        }

        protected override WorkStation CreateFromResource(WorkStationResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(WorkStation workStation, WorkStationResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<WorkStation, bool>> SearchExpression(string searchTerm)
        {
            return w => w.CitCode.ToUpper().Equals(searchTerm.ToUpper());
        }
    }
}
