namespace Linn.Production.Service.Tests.LabelPrintModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected ILabelPrintService LabelPrintService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.LabelPrintService = Substitute.For<ILabelPrintService>();
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.LabelPrintService);
                    with.Dependency<IResourceBuilder<LabelPrint>>(new LabelPrintResourceBuilder());
                    with.Module<LabelPrintModule>();
                    with.Dependency<IResourceBuilder<IdAndName>>(new IdAndNameResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<IdAndName>>>(new IdAndNameListResourceBuilder());
                    with.ResponseProcessor<LabelPrintResponseProcessor>();
                    with.ResponseProcessor<IdAndNameResponseProcessor>();
                    with.ResponseProcessor<IdAndNameListResponseProcessor>();
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
