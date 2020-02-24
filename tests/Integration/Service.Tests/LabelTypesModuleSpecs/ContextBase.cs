namespace Linn.Production.Service.Tests.LabelTypesModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
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
        protected IFacadeService<LabelType, string, LabelTypeResource, LabelTypeResource> LabelTypeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.LabelTypeService = Substitute.For<IFacadeService<LabelType, string, LabelTypeResource, LabelTypeResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.LabelTypeService);
                    with.Dependency<IResourceBuilder<LabelType>>(new LabelTypeResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<LabelType>>>(
                        new LabelTypesResourceBuilder());
                    with.Module<LabelTypeModule>();
                    with.ResponseProcessor<LabelTypesResponseProcessor>();
                    with.ResponseProcessor<LabelTypeResponseProcessor>();
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
