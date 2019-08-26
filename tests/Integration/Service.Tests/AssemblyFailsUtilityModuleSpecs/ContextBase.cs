namespace Linn.Production.Service.Tests.AssemblyFailsUtilityModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Proxy;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeService<AssemblyFailFaultCode, string, AssemblyFailFaultCodeResource,
            AssemblyFailFaultCodeResource> faultCodeService;

        protected IFacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource> FacadeService
        {
            get;
            private set;
        }

        [SetUp]
        public void EstablishContext()
        {
            this.FacadeService = Substitute
                .For<IFacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource>>();
            this.faultCodeService = Substitute
                .For<IFacadeService<AssemblyFailFaultCode, string, AssemblyFailFaultCodeResource, AssemblyFailFaultCodeResource>>();
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.FacadeService);
                    with.Dependency(this.faultCodeService);
                    with.Dependency<IResourceBuilder<AssemblyFail>>(new AssemblyFailResourceBuilder());
                    with.Dependency<IResourceBuilder<AssemblyFailFaultCode>>(
                        new AssemblyFailFaultCodeResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<AssemblyFailFaultCode>>>(new AssemblyFailFaultCodesResourceBuilder());
                    with.Module<AssemblyFailsModule>();
                    with.ResponseProcessor<AssemblyFailResponseProcessor>();
                    with.ResponseProcessor<AssemblyFailFaultCodesResponseProcessor>();
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