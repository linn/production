namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IOutstandingWorksOrdersReportFacade OutstandingWorksOrdersReportFacade { get; set; }

        protected IFacadeService<WorksOrder, int, WorksOrderResource, WorksOrderResource> WorksOrderService
        {
            get;
            set;
        }

        [SetUp]
        public void EstablishContext()
        {
            this.OutstandingWorksOrdersReportFacade = Substitute.For<IOutstandingWorksOrdersReportFacade>();
            this.WorksOrderService = Substitute.For<IFacadeService<WorksOrder, int, WorksOrderResource, WorksOrderResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.OutstandingWorksOrdersReportFacade);
                        with.Dependency(this.WorksOrderService);
                        with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());

                        with.Module<WorksOrdersModule>();
                        with.ResponseProcessor<ResultsModelJsonResponseProcessor>();
                        with.ResponseProcessor<IEnumerableCsvResponseProcessor>();
                        with.Dependency<IResourceBuilder<WorksOrder>>(
                            new WorksOrderResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<WorksOrder>>>(
                            new WorksOrdersResourceBuilder());
                        with.ResponseProcessor<WorksOrderResponseProcessor>();
                        with.ResponseProcessor<WorksOrdersResponseProcessor>();

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
