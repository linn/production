namespace Linn.Production.Domain.Tests.WhoBuiltWhatSpecs
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WhoBuiltWhatReport Sut { get; set; }

        protected IRepository<WhoBuiltWhat, string> WhoBuiltWhatRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WhoBuiltWhatRepository = Substitute.For<IRepository<WhoBuiltWhat, string>>();
            this.ReportingHelper = new ReportingHelper();
            this.Sut = new WhoBuiltWhatReport(this.WhoBuiltWhatRepository, this.ReportingHelper);
        }
    }
}