namespace Linn.Production.Service.Tests.BuildPlansModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource> BuildPlanFacadeService
        {
            get;
            private set;
        }

        protected IBuildPlansReportFacadeService BuildPlansReportFacadeService { get; private set; }

        protected IBuildPlanRulesFacadeService BuildPlanRulesFacadeService { get; private set; }

        protected IFacadeService<BuildPlanDetail, BuildPlanDetailKey, BuildPlanDetailResource, BuildPlanDetailResource> BuildPlanDetailsFacadeService
        {
            get;
            private set;
        }

        protected IAuthorisationService AuthorisationService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.BuildPlanFacadeService =
                Substitute.For<IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource>>();
            this.BuildPlansReportFacadeService = Substitute.For<IBuildPlansReportFacadeService>();
            this.BuildPlanRulesFacadeService = Substitute.For<IBuildPlanRulesFacadeService>();
            this.BuildPlanDetailsFacadeService = Substitute
                .For<IFacadeService<BuildPlanDetail, BuildPlanDetailKey, BuildPlanDetailResource, BuildPlanDetailResource>>();
            this.AuthorisationService = Substitute.For<IAuthorisationService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.BuildPlanFacadeService);
                        with.Dependency(this.BuildPlansReportFacadeService);
                        with.Dependency(this.BuildPlanRulesFacadeService);
                        with.Dependency(this.BuildPlanDetailsFacadeService);
                        with.Dependency<IResourceBuilder<ResponseModel<BuildPlan>>>(
                            new BuildPlanResourceBuilder(this.AuthorisationService));
                        with.Dependency<IResourceBuilder<ResponseModel<IEnumerable<BuildPlan>>>>(
                            new BuildPlansResourceBuilder(this.AuthorisationService));
                        with.Dependency<IResourceBuilder<ResponseModel<BuildPlanRule>>>(
                            new BuildPlanRuleResourceBuilder(this.AuthorisationService));
                        with.Dependency<IResourceBuilder<ResponseModel<IEnumerable<BuildPlanRule>>>>(
                            new BuildPlanRulesResourceBuilder(this.AuthorisationService));
                        with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());
                        with.Dependency<IResourceBuilder<ResponseModel<BuildPlanDetail>>>(
                            new BuildPlanDetailResourceBuilder(this.AuthorisationService));
                        with.Dependency<IResourceBuilder<ResponseModel<IEnumerable<BuildPlanDetail>>>>(
                            new BuildPlanDetailsResourceBuilder(this.AuthorisationService));
                        with.Module<BuildPlansModule>();
                        with.ResponseProcessor<BuildPlanResponseProcessor>();
                        with.ResponseProcessor<BuildPlansResponseProcessor>();
                        with.ResponseProcessor<BuildPlanRuleResponseProcessor>();
                        with.ResponseProcessor<BuildPlanRulesResponseProcessor>();
                        with.ResponseProcessor<BuildPlanDetailResponseProcessor>();
                        with.ResponseProcessor<BuildPlanDetailsResponseProcessor>();
                        with.ResponseProcessor<ResultsModelJsonResponseProcessor>();
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
