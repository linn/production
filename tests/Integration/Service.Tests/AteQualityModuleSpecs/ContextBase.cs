namespace Linn.Production.Service.Tests.AteQualityModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
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
        protected IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource> AteFaultCodeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.AteFaultCodeService = Substitute.For<IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.AteFaultCodeService);
                    with.Dependency<IResourceBuilder<AteFaultCode>>(new AteFaultCodeResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<AteFaultCode>>>(
                        new AteFaultCodesResourceBuilder());
                    with.Module<AteQualityModule>();
                    with.ResponseProcessor<AteFaultCodeResponseProcessor>();
                    with.ResponseProcessor<AteFaultCodesResponseProcessor>();
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