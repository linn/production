namespace Linn.Production.Service.Tests.PartCadInfoModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Authorisation;
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
        protected IFacadeService<PartCadInfo, int, PartCadInfoResource, PartCadInfoResource> PartCadInfoService { get; private set; }

        protected IRepository<PartCadInfo, int> PartCadInfoRepository { get; private set; }

        protected IAuthorisationService AuthorisationService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.PartCadInfoService = Substitute
                .For<IFacadeService<PartCadInfo, int, PartCadInfoResource, PartCadInfoResource>>();
            this.PartCadInfoRepository = Substitute.For<IRepository<PartCadInfo, int>>();
            this.AuthorisationService = Substitute.For<IAuthorisationService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.PartCadInfoService);
                        with.Dependency(this.PartCadInfoRepository);
                        with.Dependency(this.AuthorisationService);
                        with.Dependency<IResourceBuilder<ResponseModel<PartCadInfo>>>(
                            new PartCadInfoResourceBuilder(this.AuthorisationService));
                        with.Module<PartCadInfoModule>();
                        with.ResponseProcessor<PartCadInfoResponseProcessor>();
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
