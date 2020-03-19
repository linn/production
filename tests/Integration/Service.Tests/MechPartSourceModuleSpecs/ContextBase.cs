namespace Linn.Production.Service.Tests.MechPartSourceModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource> MechPartSourceService { get; private set; }

        protected IRepository<MechPartSource, int> MechPartSourceRepository { get; private set; }


        [SetUp]
        public void EstablishContext()
        {
            this.MechPartSourceService = Substitute
                .For<IFacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource>>();

            this.MechPartSourceRepository = Substitute.For<IRepository<MechPartSource, int>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.MechPartSourceService);
                        with.Dependency(this.MechPartSourceRepository);
                        with.Dependency<IResourceBuilder<MechPartSource>>(new MechPartSourceResourceBuilder());
                        with.Module<MechPartSourceModule>();
                        with.ResponseProcessor<MechPartSourceResponseProcessor>();
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
