﻿namespace Linn.Production.Service.Tests.DeliveryPerformanceSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IDeliveryPerfResultFacadeService DeliveryPerfResultFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.DeliveryPerfResultFacadeService = Substitute.For<IDeliveryPerfResultFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.DeliveryPerfResultFacadeService);
                    with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<ResultsModel>>>(
                        new ResultsModelsResourceBuilder());
                    with.Module<DeliveryPerformanceModule>();

                    with.ResponseProcessor<ResultsModelsJsonResponseProcessor>();
                    with.ResponseProcessor<ResultsModelJsonResponseProcessor>();
                    with.ResponseProcessor<IEnumerableCsvResponseProcessor>();

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
