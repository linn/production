namespace Linn.Production.Service.Tests.ProductionMeasuresModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Production.Domain;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IProductionMeasuresReportFacade ProductionMeasuresReportFacade { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ProductionMeasuresReportFacade = Substitute.For<IProductionMeasuresReportFacade>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ProductionMeasuresReportFacade);
                    with.Dependency<IResourceBuilder<ProductionMeasures>>(new ProductionMeasuresResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<ProductionMeasures>>>(
                        new ProductionMeasuresListResourceBuilder());
                    with.Module<ProductionMeasuresModule>();
                    with.ResponseProcessor<ProductionMeasuresResponseProcessor>();
                    with.ResponseProcessor<ProductionMeasuresListResponseProcessor>();
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
