namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class BuildPlanDetailsReportLineRepository : IQueryRepository<BuildPlanDetailsReportLine>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BuildPlanDetailsReportLineRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public BuildPlanDetailsReportLine FindBy(Expression<Func<BuildPlanDetailsReportLine, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BuildPlanDetailsReportLine> FilterBy(Expression<Func<BuildPlanDetailsReportLine, bool>> expression)
        {
            return this.serviceDbContext.BuildPlanDetailsReportLines.Where(expression);
        }

        public IQueryable<BuildPlanDetailsReportLine> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}