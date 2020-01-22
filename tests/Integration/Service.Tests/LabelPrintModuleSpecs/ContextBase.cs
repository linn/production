namespace Linn.Production.Service.Tests.LabelPrintModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected ILabelPrintService LabelPrintService { get; private set; }

        protected IFacadeService<Address, int, AddressResource, AddressResource> AddressService { get; set; }

        protected IFacadeService<Supplier, int, SupplierResource, SupplierResource> SupplierService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.LabelPrintService = Substitute.For<ILabelPrintService>();
            this.AddressService = Substitute.For<IFacadeService<Address, int, AddressResource, AddressResource>>();
            this.SupplierService =
                Substitute.For<IFacadeService<Supplier, int, SupplierResource, SupplierResource>>();
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.LabelPrintService);
                    with.Dependency(this.AddressService);
                    with.Dependency(this.SupplierService);
                    with.Dependency<IResourceBuilder<LabelPrint>>(new LabelPrintResourceBuilder());
                    with.Module<LabelPrintModule>();
                    with.Dependency<IResourceBuilder<IdAndName>>(new IdAndNameResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<IdAndName>>>(new IdAndNameListResourceBuilder());
                    with.Dependency<IResourceBuilder<Address>>(new AddressResourceBuilder());
                    with.Dependency<IResourceBuilder<Supplier>>(new SupplierResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Address>>>(new AddressesResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Supplier>>>(new SuppliersResourceBuilder());
                    with.ResponseProcessor<LabelPrintResponseProcessor>();
                    with.ResponseProcessor<IdAndNameResponseProcessor>();
                    with.ResponseProcessor<IdAndNameListResponseProcessor>();
                    with.ResponseProcessor<AddressResponseProcessor>();
                    with.ResponseProcessor<SupplierResponseProcessor>();
                    with.ResponseProcessor<AddressesResponseProcessor>();
                    with.ResponseProcessor<SuppliersResponseProcessor>();
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
