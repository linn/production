namespace Linn.Production.Service.Tests.PartCadInfoModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeService<Part, string, PartResource, PartResource> PartsService { get; private set; }

        protected IRepository<Part, string> PartsRepository { get; private set; }

        protected IAuthorisationService AuthorisationService { get; private set; }

        private IWorksOrderMessageService WorksOrderMessageService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.PartsService = Substitute.For<IFacadeService<Part, string, PartResource, PartResource>>();
            this.PartsRepository = Substitute.For<IRepository<Part, string>>();
            this.AuthorisationService = Substitute.For<IAuthorisationService>();
            this.WorksOrderMessageService = Substitute.For<IWorksOrderMessageService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.PartsService);
                    with.Dependency(this.PartsRepository);
                    with.Dependency(this.AuthorisationService);
                    with.Dependency<IResourceBuilder<ResponseModel<Part>>>(new PartResourceBuilder(this
                        .AuthorisationService, this.WorksOrderMessageService));
                    with.Dependency<IResourceBuilder<ResponseModel<IEnumerable<Part>>>>(
                        new PartsResourceBuilder(this.AuthorisationService, this.WorksOrderMessageService));
                    with.Module<PartCadInfoModule>();
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
