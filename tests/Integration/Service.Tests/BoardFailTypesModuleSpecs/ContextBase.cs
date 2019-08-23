namespace Linn.Production.Service.Tests.BoardFailTypesModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeService<BoardFailType, int, BoardFailTypeResource, BoardFailTypeResource> FacadeService
        {
            get;
            private set;
        }

        [SetUp]
        public void EstablishContext()
        {
            this.FacadeService = Substitute
                .For<IFacadeService<BoardFailType, int, BoardFailTypeResource, BoardFailTypeResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.FacadeService);
                    with.Dependency<IResourceBuilder<BoardFailType>>(new BoardFailTypeResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<BoardFailType>>>(
                        new BoardFailTypesResourceBuilder());
                    with.Module<BoardFailTypesModule>();
                    with.ResponseProcessor<BoardFailTypeResponseProcessor>();
                    with.ResponseProcessor<BoardFailTypesResponseProcessor>();
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