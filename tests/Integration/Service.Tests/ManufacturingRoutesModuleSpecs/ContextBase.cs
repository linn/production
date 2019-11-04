namespace Linn.Production.Service.Tests.ManufacturingRoutesModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Authorisation;
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
        protected IFacadeService<ManufacturingRoute, string, ManufacturingRouteResource, ManufacturingRouteResource> ManufacturingRouteService { get; private set; }
        protected IAuthorisationService AuthorisationService;
        [SetUp]
        public void EstablishContext()
        {
            this.ManufacturingRouteService = Substitute.For<IFacadeService<ManufacturingRoute, string, ManufacturingRouteResource, ManufacturingRouteResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ManufacturingRouteService);
                    with.Dependency<IResourceBuilder<ManufacturingRoute>>(ManufacturingRouteResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<ManufacturingRoute>>>(
                        new ManufacturingRoutesResourceBuilder());
                    with.Module<ManufacturingRoutesModule>();
                    with.ResponseProcessor<ManufacturingRoutesResponseProcessor>();
                    with.ResponseProcessor<ManufacturingRouteResponseProcessor>();
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
