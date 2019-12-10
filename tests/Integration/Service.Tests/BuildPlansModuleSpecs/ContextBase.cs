namespace Linn.Production.Service.Tests.BuildPlansModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

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

        [SetUp]
        public void EstablishContext()
        {
            this.BuildPlanFacadeService =
                Substitute.For<IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource>>();
            this.BuildPlansReportFacadeService = Substitute.For<IBuildPlansReportFacadeService>();
            this.BuildPlanRulesFacadeService = Substitute.For<IBuildPlanRulesFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.BuildPlanFacadeService);
                        with.Dependency(this.BuildPlansReportFacadeService);
                        with.Dependency(this.BuildPlanRulesFacadeService);
                        with.Dependency<IResourceBuilder<BuildPlan>>(new BuildPlanResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<BuildPlan>>>(new BuildPlansResourceBuilder());
                        with.Dependency<IResourceBuilder<BuildPlanRule>>(new BuildPlanRuleResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<BuildPlanRule>>>(new BuildPlanRulesResourceBuilder());
                        with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());
                        with.Module<BuildPlansModule>();
                        with.ResponseProcessor<BuildPlanResponseProcessor>();
                        with.ResponseProcessor<BuildPlansResponseProcessor>();
                        with.ResponseProcessor<BuildPlanRuleResponseProcessor>();
                        with.ResponseProcessor<BuildPlanRulesResponseProcessor>();
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
