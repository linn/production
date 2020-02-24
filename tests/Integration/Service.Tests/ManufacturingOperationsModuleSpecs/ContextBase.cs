namespace Linn.Production.Service.Tests.ManufacturingOperationsModuleSpecs
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
        protected IFacadeService<ManufacturingOperation, int, ManufacturingOperationResource, ManufacturingOperationResource> ManufacturingOperationService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ManufacturingOperationService = Substitute.For<IFacadeService<ManufacturingOperation, int, ManufacturingOperationResource, ManufacturingOperationResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ManufacturingOperationService);
                    with.Dependency<IResourceBuilder<ManufacturingOperation>>(new ManufacturingOperationResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<ManufacturingOperation>>>(
                        new ManufacturingOperationsResourceBuilder());
                    with.Module<ManufacturingOperationsModule>();
                    with.ResponseProcessor<ManufacturingOperationsResponseProcessor>();
                    with.ResponseProcessor<ManufacturingOperationResponseProcessor>();
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
