namespace Linn.Production.Service.Tests.WorkStationsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;
    using Linn.Production.Service.Tests;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IFacadeService<WorkStation, string, WorkStationResource, WorkStationResource> WorkStationService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.WorkStationService = Substitute.For<IFacadeService<WorkStation, string, WorkStationResource, WorkStationResource>>();
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.WorkStationService);
                    with.Dependency<IResourceBuilder<WorkStation>>(new WorkStationResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<WorkStation>>>(
                        new WorkStationsResourceBuilder());
                    with.Module<WorkStationModule>();
                    with.ResponseProcessor<WorkStationsResponseProcessor>();
                    with.ResponseProcessor<WorkStationResponseProcessor>();
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
