namespace Linn.Production.Domain.Tests.AssemblyFailsReportsSpecs
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.Repositories;
    using Linn.Production.Domain.LinnApps.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AssemblyFailsReportService Sut { get; set; }

        protected ILinnWeekPack LinnWeekPack { get; private set; }

        protected ILinnWeekService LinnWeekService { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        protected IRepository<AssemblyFail, int> AssemblyFailRepository { get; private set; }

        protected ILinnWeekRepository LinnWeekRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.LinnWeekPack = Substitute.For<ILinnWeekPack>();
            this.LinnWeekRepository = Substitute.For<ILinnWeekRepository>();
            this.LinnWeekService = new LinnWeekService(this.LinnWeekRepository);
            this.AssemblyFailRepository = Substitute.For<IRepository<AssemblyFail, int>>();
            this.ReportingHelper = new ReportingHelper();
            this.Sut = new AssemblyFailsReportService(
                this.LinnWeekPack,
                this.AssemblyFailRepository,
                this.LinnWeekService,
                this.ReportingHelper);
        }

    }
}