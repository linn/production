﻿namespace Linn.Production.Service.Tests.ShortageModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IShortageSummaryFacadeService ShortageSummaryFacadeService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ShortageSummaryFacadeService = Substitute.For<IShortageSummaryFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ShortageSummaryFacadeService);
                    with.Dependency<IResourceBuilder<ShortageSummary>>(new ShortageSummaryResourceBuilder());
                    with.Module<ShortageModule>();
                    with.ResponseProcessor<ShortageSummaryJsonResponseProcessor>();
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
