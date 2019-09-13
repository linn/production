namespace Linn.Production.Domain.Tests.SmtReportsSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports.Smt;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected SmtReports Sut { get; set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        protected IRepository<BomDetailExplodedPhantomPartView, int> BomDetailRepository { get; private set; }

        protected IRepository<WorksOrder, int> WorksOrdersRepository { get; private set; }

        protected List<WorksOrder> WorksOrders { get; set; }

        protected List<BomDetailExplodedPhantomPartView> BomDetails { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WorksOrdersRepository = Substitute.For<IRepository<WorksOrder, int>>();
            this.BomDetailRepository = Substitute.For<IRepository<BomDetailExplodedPhantomPartView, int>>();
            this.ReportingHelper = new ReportingHelper();

            this.WorksOrders = new List<WorksOrder>
                                   {
                                       new WorksOrder { Outstanding = "Y", QuantityOutstanding = 4, WorkStationCode = "SMT1" },
                                       new WorksOrder { Outstanding = "Y", QuantityOutstanding = 10, WorkStationCode = "SMT2" }
                                   };
            this.BomDetails = new List<BomDetailExplodedPhantomPartView>
                                  {
                                      new BomDetailExplodedPhantomPartView { BomName = "B1", Quantity = 2, PartNumber = "P1", DecrementRule = "Y" },
                                      new BomDetailExplodedPhantomPartView { BomName = "B2", Quantity = 1, PartNumber = "P2", DecrementRule = "Y" }
                                  };

            this.WorksOrdersRepository.FilterBy(Arg.Any<Expression<Func<WorksOrder, bool>>>())
                .Returns(this.WorksOrders.AsQueryable());
            this.BomDetailRepository.FilterBy(Arg.Any<Expression<Func<BomDetailExplodedPhantomPartView, bool>>>())
                .Returns(this.BomDetails.AsQueryable());

            this.Sut = new SmtReports(
                this.WorksOrdersRepository,
                this.BomDetailRepository,
                this.ReportingHelper);
        }
    }
}