namespace Linn.Production.Service.Tests.BuildPlansModule
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;
    
    public abstract class ContextBase : NancyContextBase
    {
        protected IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource> BuildPlanService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.BuildPlanService =
                Substitute.For<IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.BuildPlanService);
                        with.Dependency<IResourceBuilder<BuildPlan>>(new BuildPlanResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<BuildPlan>>>(new BuildPlansResourceBuilder());
                        with.Module<BuildPlansModule>();
                        with.ResponseProcessor<BuildPlanResponseProcessor>();
                        with.ResponseProcessor<BuildPlansResponseProcessor>();
                        with.RequestStartup(
                            (containers, pipelines, context) =>
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
