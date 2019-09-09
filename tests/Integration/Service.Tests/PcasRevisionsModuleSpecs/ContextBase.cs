namespace Linn.Production.Service.Tests.PcasRevisionsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;
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
        protected IFacadeService<PcasRevision, string, PcasRevisionResource, PcasRevisionResource> PcasRevisionService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.PcasRevisionService = Substitute.For<IFacadeService<PcasRevision, string, PcasRevisionResource, PcasRevisionResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.PcasRevisionService);
                    with.Dependency<IResourceBuilder<PcasRevision>>(new PcasRevisionResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<PcasRevision>>>(
                        new PcasRevisionsResourceBuilder());
                    with.Module<PcasRevisionsModule>();
                    with.ResponseProcessor<PcasRevisionsResponseProcessor>();
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