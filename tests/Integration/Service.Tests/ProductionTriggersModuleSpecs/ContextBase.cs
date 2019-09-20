namespace Linn.Production.Service.Tests.ProductionTriggersModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IProductionTriggersFacadeService ProductionTriggersFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ProductionTriggersFacadeService = Substitute.For<IProductionTriggersFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ProductionTriggersFacadeService);
                    with.Dependency<IResourceBuilder<ProductionTriggersReport>>(new ProductionTriggersReportResourceBuilder());
                    with.Dependency<IResourceBuilder<ProductionTriggerFacts>>(new ProductionTriggersFactsResourceBuilder());
                    with.Module<ProductionTriggersModule>();
                    with.ResponseProcessor<ProductionTriggersReportResponseProcessor>();
                    with.ResponseProcessor<ProductionTriggerFactstResponseProcessor>();
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
