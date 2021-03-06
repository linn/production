﻿namespace Linn.Production.Service.Tests.AteQualityModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
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

        protected IFacadeService<AteTestDetail, AteTestDetailKey, AteTestDetailResource, AteTestDetailResource> AteTestDetailService { get; private set; }

        protected IFacadeService<AteTest, int, AteTestResource, AteTestResource> AteTestService { get; private set; }

        protected IAteReportsFacadeService AteReportsFacadeService { get; private set; }

        protected ICountComponentsFacadeService CountComponentsService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.AteFaultCodeService = Substitute.For<IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource>>();
            this.AteTestService = Substitute.For<IFacadeService<AteTest, int, AteTestResource, AteTestResource>>();
            this.AteTestDetailService = Substitute.For<IFacadeService<AteTestDetail, AteTestDetailKey, AteTestDetailResource, AteTestDetailResource>>();
            this.CountComponentsService = Substitute.For<ICountComponentsFacadeService>();
            this.AteReportsFacadeService = Substitute.For<IAteReportsFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.AteFaultCodeService);
                    with.Dependency(this.AteTestService);
                    with.Dependency(this.AteTestDetailService);
                    with.Dependency(this.AteReportsFacadeService);
                    with.Dependency(this.CountComponentsService);
                    with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());
                    with.Dependency<IResourceBuilder<AteFaultCode>>(new AteFaultCodeResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<AteFaultCode>>>(
                        new AteFaultCodesResourceBuilder());
                    with.Dependency<IResourceBuilder<AteTest>>(new AteTestResourceBuilder());
                    with.Dependency<IResourceBuilder<ComponentCount>>(new ComponentCountResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<AteTest>>>(new AteTestsResourceBuilder());

                    with.Module<AteQualityModule>();
                    with.ResponseProcessor<AteFaultCodeResponseProcessor>();
                    with.ResponseProcessor<AteFaultCodesResponseProcessor>();
                    with.ResponseProcessor<ResultsModelJsonResponseProcessor>();
                    with.ResponseProcessor<AteTestResponseProcessor>();
                    with.ResponseProcessor<AteTestsResponseProcessor>();
                    with.ResponseProcessor<AteTestsResponseProcessor>();
                    with.ResponseProcessor<ComponentCountResponseProcessor>();

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