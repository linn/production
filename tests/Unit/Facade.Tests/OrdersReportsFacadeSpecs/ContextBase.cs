﻿namespace Linn.Production.Facade.Tests.OrdersReportsFacadeSpecs
{
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected OrdersReportsFacadeService Sut { get; set; }

        protected IOrdersReports OrdersReports { get; private set; }

        protected IOverdueOrdersService OverdueOrdersService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.OrdersReports = Substitute.For<IOrdersReports>();
            this.OverdueOrdersService = Substitute.For<IOverdueOrdersService>();
            this.Sut = new OrdersReportsFacadeService(this.OrdersReports, this.OverdueOrdersService);
        }
    }
}