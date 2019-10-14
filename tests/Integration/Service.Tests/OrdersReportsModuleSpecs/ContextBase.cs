namespace Linn.Production.Service.Tests.OrdersReportsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Modules.Reports;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IOrdersReportsFacadeService OrdersReportsFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.OrdersReportsFacadeService = Substitute.For<IOrdersReportsFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.OrdersReportsFacadeService);
                        with.Dependency<IResourceBuilder<ManufacturingCommitDateResults>>(new ManufacturingCommitDateResourceBuilder());
                        with.Module<OrdersReportsModule>();
                        with.ResponseProcessor<ManufacturingCommitDateJsonResponseProcessor>();
                        with.RequestStartup(
                            (container, pipelines, context) =>
                                {
                                    var claims = new List<Claim>
                                                     {
                                                         new Claim(ClaimTypes.Role, "employee"),
                                                         new Claim(ClaimTypes.NameIdentifier, "test-user")
                                                     };

                                    var user = new ClaimsIdentity(claims, "jwt");

                                    context.CurrentUser = new ClaimsPrincipal(user);
                                });
                    });

            this.Browser = new Browser(bootstrapper);
        }
    }
}