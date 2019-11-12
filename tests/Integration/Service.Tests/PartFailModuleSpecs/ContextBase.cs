namespace Linn.Production.Service.Tests.PartFailModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;
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
        protected IFacadeService<PartFail, int, PartFailResource, PartFailResource> FacadeService { get; private set; }

        protected IFacadeService<PartFailFaultCode, string, PartFailFaultCodeResource, PartFailFaultCodeResource> FaultCodeService { get; private set; }

        protected IFacadeService<PartFailErrorType, string, PartFailErrorTypeResource, PartFailErrorTypeResource> ErrorTypeService { get; private set; }

        protected IPartsReportFacadeService PartsReportFacadeService { get; private set; }

        protected IPartFailSupplierService PartFailSupplierService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.FacadeService = Substitute
                .For<IFacadeService<PartFail, int, PartFailResource, PartFailResource>>();
            this.ErrorTypeService =
                Substitute
                    .For<IFacadeService<PartFailErrorType, string, PartFailErrorTypeResource, PartFailErrorTypeResource>>();
            this.FaultCodeService =
                Substitute
                    .For<IFacadeService<PartFailFaultCode, string, PartFailFaultCodeResource, PartFailFaultCodeResource>>();
            this.PartsReportFacadeService = Substitute.For<IPartsReportFacadeService>();
            this.PartFailSupplierService = Substitute.For<IPartFailSupplierService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.FacadeService);
                    with.Dependency(this.FaultCodeService);
                    with.Dependency(this.ErrorTypeService);
                    with.Dependency(this.PartsReportFacadeService);
                    with.Dependency(this.PartFailSupplierService);
                    with.Dependency<IResourceBuilder<PartFail>>(new PartFailResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<PartFail>>>(new PartFailsResourceBuilder());
                    with.Dependency<IResourceBuilder<PartFailErrorType>>(new PartFailErrorTypeResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<PartFailErrorType>>>(new PartFailErrorTypesResourceBuilder());
                    with.Dependency<IResourceBuilder<PartFailFaultCode>>(new PartFailFaultCodeResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<PartFailFaultCode>>>(new PartFailFaultCodesResourceBuilder());
                    with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<ResultsModel>>>(
                        new ResultsModelsResourceBuilder());
                    with.Dependency<IResourceBuilder<PartFailSupplierView>>(new PartFailSupplierResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<PartFailSupplierView>>>(
                        new PartFailSuppliersResourceBuilder());
                    with.Module<PartFailsModule>();
                    with.ResponseProcessor<PartFailResponseProcessor>();
                    with.ResponseProcessor<PartFailsResponseProcessor>();
                    with.ResponseProcessor<PartFailErrorTypeResponseProcessor>();
                    with.ResponseProcessor<PartFailErrorTypesResponseProcessor>();
                    with.ResponseProcessor<PartFailFaultCodeResponseProcessor>();
                    with.ResponseProcessor<PartFailFaultCodesResponseProcessor>();
                    with.ResponseProcessor<ResultsModelsJsonResponseProcessor>();
                    with.ResponseProcessor<ResultsModelJsonResponseProcessor>();
                    with.ResponseProcessor<PartFailSupplierResponseProcessor>();
                    with.ResponseProcessor<PartFailSuppliersResponseProcessor>();
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