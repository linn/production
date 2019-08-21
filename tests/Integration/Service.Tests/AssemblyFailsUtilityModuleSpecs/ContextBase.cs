namespace Linn.Production.Service.Tests.AssemblyFailsUtilityModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Proxy;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected ISalesArticleService salesArticleService;

        protected IFacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource> FacadeService
        {
            get;
            private set;
        }

        [SetUp]
        public void EstablishContext()
        {
            this.FacadeService = Substitute
                .For<IFacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource>>();
            this.salesArticleService = Substitute.For<ISalesArticleService>();
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.FacadeService);
                    with.Dependency<IResourceBuilder<AssemblyFail>>(new AssemblyFailResourceBuilder());
                    
                    with.Module<AssemblyFailsModule>();
                    with.ResponseProcessor<AssemblyFailResponseProcessor>();
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