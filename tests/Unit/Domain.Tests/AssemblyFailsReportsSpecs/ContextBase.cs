namespace Linn.Production.Domain.Tests.AssemblyFailsReportsSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Reports;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AssemblyFailsReportService Sut { get; set; }

        protected ILinnWeekPack LinnWeekPack { get; private set; }

        protected IRepository<AssemblyFail, int> AssemblyFailRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.LinnWeekPack = Substitute.For<ILinnWeekPack>();
            this.AssemblyFailRepository = Substitute.For<IRepository<AssemblyFail, int>>();
            this.Sut = new AssemblyFailsReportService(this.LinnWeekPack, this.AssemblyFailRepository);
        }
    }
}