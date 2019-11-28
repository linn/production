namespace Linn.Production.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IPartsFacadeService PartsFacadeService;

        protected IRepository<Part, string> PartRepository;

        [SetUp]
        public void EstablishContext()
        {
            this.PartsFacadeService = Substitute.For<IPartsFacadeService>();

            this.PartRepository = Substitute.For<IRepository<Part, string>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.PartsFacadeService);
                    with.Dependency(this.PartRepository);
                    with.Dependency<IResourceBuilder<Part>>(new PartResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Part>>>(new PartsResourceBuilder());
                    with.Module<PartsModule>();
                    with.ResponseProcessor<PartResponseProcessor>();
                    with.ResponseProcessor<PartsResponseProcessor>();
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