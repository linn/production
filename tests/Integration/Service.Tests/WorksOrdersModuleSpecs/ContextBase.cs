﻿namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IOutstandingWorksOrdersReportFacade OutstandingWorksOrdersReportFacade { get; private set; }

        protected IFacadeService<WorksOrderLabel, WorksOrderLabelKey, WorksOrderLabelResource, WorksOrderLabelResource> LabelService
        {
            get;
            private set;
        }

        protected IWorksOrdersService WorksOrdersService { get; private set; }

        protected IWorksOrderLabelPack WorksOrderLabelPack { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.OutstandingWorksOrdersReportFacade = Substitute.For<IOutstandingWorksOrdersReportFacade>();
            this.LabelService = Substitute
                .For<IFacadeService<WorksOrderLabel, WorksOrderLabelKey, WorksOrderLabelResource, WorksOrderLabelResource>>();
            this.WorksOrdersService = Substitute.For<IWorksOrdersService>();
            this.WorksOrderLabelPack = Substitute.For<IWorksOrderLabelPack>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.OutstandingWorksOrdersReportFacade);
                        with.Dependency(this.WorksOrdersService);
                        with.Dependency(this.LabelService);
                        with.Dependency(this.WorksOrderLabelPack);
                        with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());
                        with.Dependency<IResourceBuilder<WorksOrder>>(new WorksOrderResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<WorksOrder>>>(
                            new WorksOrdersResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<WorksOrder>>>(new WorksOrdersResourceBuilder());
                        with.Dependency<IResourceBuilder<WorksOrderPartDetails>>(new WorksOrderPartDetailsResourceBuilder());
                        with.Dependency<IResourceBuilder<WorksOrderLabel>>(new WorksOrderLabelResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<WorksOrderLabel>>>(new WorksOrderLabelsResourceBuilder());
                        with.Dependency<IResourceBuilder<Error>>(new ErrorResourceBuilder());

                        with.Module<WorksOrdersModule>();
                        with.ResponseProcessor<ResultsModelJsonResponseProcessor>();
                        with.ResponseProcessor<IEnumerableCsvResponseProcessor>();
                        with.ResponseProcessor<WorksOrderResponseProcessor>();
                        with.ResponseProcessor<WorksOrdersResponseProcessor>();
                        with.ResponseProcessor<WorksOrderPartDetailsResponseProcessor>();
                        with.ResponseProcessor<WorksOrderLabelResponseProcessor>();
                        with.ResponseProcessor<WorksOrderLabelsResponseProcessor>();
                        with.ResponseProcessor<ErrorResponseProcessor>();

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
