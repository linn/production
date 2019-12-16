namespace Linn.Production.Service.Tests.ProductionTriggerLevelsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Dispatchers;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;
    using Linn.Production.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IProductionTriggerLevelsService ProductionTriggerLevelService { get; private set; }

        protected ISingleRecordFacadeService<PtlSettings, PtlSettingsResource> PtlSettingsFacadeService { get; private set; }

        protected IAuthorisationService AuthorisationService { get; private set; }

        protected ITriggerRunDispatcher TriggerRunDispatcher { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ProductionTriggerLevelService = Substitute.For<IProductionTriggerLevelsService>();
            this.PtlSettingsFacadeService = Substitute.For<ISingleRecordFacadeService<PtlSettings, PtlSettingsResource>>();
            this.AuthorisationService = Substitute.For<IAuthorisationService>();
            this.TriggerRunDispatcher = Substitute.For<ITriggerRunDispatcher>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ProductionTriggerLevelService);
                    with.Dependency(this.PtlSettingsFacadeService);
                    with.Dependency(this.AuthorisationService);
                    with.Dependency(this.TriggerRunDispatcher);
                    with.Dependency<IResourceBuilder<Error>>(new ErrorResourceBuilder());
                    with.Dependency<IResourceBuilder<ResponseModel<PtlSettings>>>(new PtlSettingsResourceBuilder(this.AuthorisationService));
                    with.Dependency<IResourceBuilder<ResponseModel<IEnumerable<ProductionTriggerLevel>>>>(
                        new ProductionTriggerLevelsResourceBuilder(this.AuthorisationService));
                    with.Module<ProductionTriggerLevelsModule>();
                    with.Dependency<IResourceBuilder<ResponseModel<ProductionTriggerLevel>>>(new ProductionTriggerLevelResourceBuilder(this.AuthorisationService));

                    with.ResponseProcessor<ProductionTriggerLevelResponseProcessor>();
                    with.ResponseProcessor<ProductionTriggerLevelsResponseProcessor>();
                    with.ResponseProcessor<ErrorResponseProcessor>();
                    with.ResponseProcessor<PtlSettingsResponseProcessor>();
                    with.RequestStartup(
                        (container, pipelines, context) =>
                        {
                            var claims = new List<Claim>
                                                 {
                                                         new Claim(ClaimTypes.Role, "employee"),
                                                         new Claim(ClaimTypes.NameIdentifier, "test-user"),
                                                         new Claim("employee", "/e/111")
                                                 };

                            var user = new ClaimsIdentity(claims, "jwt");

                            context.CurrentUser = new ClaimsPrincipal(user);
                        });
                });

            this.Browser = new Browser(bootstrapper);
        }
    }
}