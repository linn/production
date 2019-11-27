namespace Linn.Production.Domain.Tests.BuildPlansReportSpecs
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.Repositories;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected BuildPlansReportService Sut { get; set; }

        protected IQueryRepository<BuildPlanDetailsReportLine> BuildPlanDetailsLineRepository { get; private set; }

        protected ILinnWeekService LinnWeekService { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        protected ILinnWeekRepository LinnWeekRepository { get; private set; }

        [SetUp]
        public void SetUp()
        {
            this.BuildPlanDetailsLineRepository = Substitute.For<IQueryRepository<BuildPlanDetailsReportLine>>();
            this.LinnWeekRepository = Substitute.For<ILinnWeekRepository>();
            this.LinnWeekService = new LinnWeekService(this.LinnWeekRepository);
            this.ReportingHelper = new ReportingHelper();

            this.Sut = new BuildPlansReportService(
                this.BuildPlanDetailsLineRepository,
                this.LinnWeekService,
                this.ReportingHelper);
        }
    }
}
