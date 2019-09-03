namespace Linn.Production.Service.Tests.CitsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
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
        protected IFacadeService<Cit, string, CitResource, CitResource> CitService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.CitService = Substitute.For<IFacadeService<Cit, string, CitResource, CitResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.CitService);
                        with.Dependency<IResourceBuilder<Cit>>(new CitResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<Cit>>>(new CitsResourceBuilder());
                        with.Module<CitsModule>();
                        with.ResponseProcessor<CitsResponseProcessor>();
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