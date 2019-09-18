namespace Linn.Production.Service.Tests.ProductionTriggerLevelsModuleSpecs
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
        protected IFacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource> ProductionTriggerLevelService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ProductionTriggerLevelService = Substitute.For<IFacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ProductionTriggerLevelService);
                    with.Dependency<IResourceBuilder<ProductionTriggerLevel>>(new ProductionTriggerLevelResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<ProductionTriggerLevel>>>(
                        new ProductionTriggerLevelsResourceBuilder());
                    with.Module<ProductionTriggerLevelsModule>();
                    with.ResponseProcessor<ProductionTriggerLevelResponseProcessor>();
                    with.ResponseProcessor<ProductionTriggerLevelsResponseProcessor>();
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