namespace Linn.Production.Service.Tests.AssemblyFailsUtilityModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
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
        protected IFacadeService<AssemblyFailFaultCode, string, AssemblyFailFaultCodeResource,
            AssemblyFailFaultCodeResource> FaultCodeService
        {
            get; private set;
        }


        protected IAssemblyFailsService FacadeService
        {
            get;
            private set;
        }

        [SetUp]
        public void EstablishContext()
        {
            this.FacadeService = Substitute
                .For<IAssemblyFailsService>();

            this.FaultCodeService = Substitute
                .For<IFacadeService<AssemblyFailFaultCode, string, AssemblyFailFaultCodeResource, AssemblyFailFaultCodeResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.FacadeService);
                    with.Dependency(this.FaultCodeService);
                    with.Dependency<IResourceBuilder<AssemblyFail>>(new AssemblyFailResourceBuilder());
                    with.Dependency<IResourceBuilder<AssemblyFailFaultCode>>(
                        new AssemblyFailFaultCodeResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<AssemblyFailFaultCode>>>(new AssemblyFailFaultCodesResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<AssemblyFail>>>(new AssemblyFailsResourceBuilder());
                    with.Module<AssemblyFailsModule>();
                    with.ResponseProcessor<AssemblyFailResponseProcessor>();
                    with.ResponseProcessor<AssemblyFailsResponseProcessor>();
                    with.ResponseProcessor<AssemblyFailFaultCodeResponseProcessor>();
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