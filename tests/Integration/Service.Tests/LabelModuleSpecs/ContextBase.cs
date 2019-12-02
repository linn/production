namespace Linn.Production.Service.Tests.LabelModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected ILabelService LabelService { get; private set; }

        protected IFacadeService<LabelReprint, int, LabelReprintResource, LabelReprintResource> LabelReprintFacadeService { get; private set; }


    [SetUp]
        public void EstablishContext()
        {
            this.LabelService = Substitute.For<ILabelService>();
            this.LabelReprintFacadeService = Substitute.For<IFacadeService<LabelReprint, int, LabelReprintResource, LabelReprintResource>>();
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.LabelService);
                    with.Dependency(this.LabelReprintFacadeService);
                    with.Dependency<IResourceBuilder<Error>>(new ErrorResourceBuilder());
                    with.Dependency<IResourceBuilder<LabelReprint>>(new LabelReprintResourceBuilder());
                    with.Module<LabelsModule>();
                    with.ResponseProcessor<ErrorResponseProcessor>();
                    with.ResponseProcessor<LabelReprintResponseProcessor>();
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