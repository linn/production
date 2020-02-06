namespace Linn.Production.Service.Tests.ProductionTriggersModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
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

        private IRepository<AteTest, int> AteTestRepository { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ProductionTriggersFacadeService = Substitute.For<IProductionTriggersFacadeService>();
            this.AteTestRepository = Substitute.For<IRepository<AteTest, int>>();

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
