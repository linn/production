namespace Linn.Production.Service.Tests.BoardTestsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.BoardTests;
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
        protected IFacadeService<BoardFailType, int, BoardFailTypeResource, BoardFailTypeResource> FacadeService { get; private set; }

        protected IBoardTestReportFacadeService BoardTestReportFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.FacadeService = Substitute.For<IFacadeService<BoardFailType, int, BoardFailTypeResource, BoardFailTypeResource>>();
            this.BoardTestReportFacadeService = Substitute.For<IBoardTestReportFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.FacadeService);
                    with.Dependency(this.BoardTestReportFacadeService);
                    with.Dependency<IResourceBuilder<BoardFailType>>(new BoardFailTypeResourceBuilder());
                    with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<BoardFailType>>>(
                        new BoardFailTypesResourceBuilder());
                    with.Module<BoardTestsModule>();
                    with.ResponseProcessor<BoardFailTypeResponseProcessor>();
                    with.ResponseProcessor<BoardFailTypesResponseProcessor>();
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