namespace Linn.Production.Service.Tests.PurchaseOrderModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IPurchaseOrderService FacadeService { get; private set; }

        protected ISernosPack SernosPack { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.FacadeService = Substitute.For<IPurchaseOrderService>();
            this.SernosPack = Substitute.For<ISernosPack>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.FacadeService);
                        with.Dependency<IResourceBuilder<PurchaseOrder>>(
                            new PurchaseOrderResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<PurchaseOrder>>>(
                            new PurchaseOrdersResourceBuilder());
                        with.Dependency<IResourceBuilder<PurchaseOrderWithSernosInfo>>(
                            new PurchaseOrderWithSernosInfoResourceBuilder());
                        with.Dependency<IResourceBuilder<PurchaseOrderDetail>>(
                            new PurchaseOrderDetailResourceBuilder());
                        with.Dependency<ISernosPack>(this.SernosPack);
                        with.Module<PurchaseOrdersModule>();

                        with.ResponseProcessor<PurchaseOrderResponseProcessor>();
                        with.ResponseProcessor<PurchaseOrdersResponseProcessor>();
                        with.ResponseProcessor<PurchaseOrderWithSernosInfoResponseProcessor>();

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