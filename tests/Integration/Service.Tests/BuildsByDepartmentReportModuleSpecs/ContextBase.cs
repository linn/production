namespace Linn.Production.Service.Tests.BuildsByDepartmentReportModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Facade.ResourceBuilders;
    using Facade.Services;
    using Modules.Reports;
    using ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IBuildsByDepartmentReportFacadeService BuildsByDepartmentReportFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.BuildsByDepartmentReportFacadeService = Substitute.For<IBuildsByDepartmentReportFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.BuildsByDepartmentReportFacadeService);
                        with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<ResultsModel>>>(
                            new ResultsModelsResourceBuilder());
                        with.Module<BuildsByDepartmentReportModule>();

                        with.ResponseProcessor<ResultsModelsJsonResponseProcessor>();


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