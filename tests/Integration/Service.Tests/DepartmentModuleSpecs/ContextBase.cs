namespace Linn.Production.Service.Tests.DepartmentModuleSpecs
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
        protected IFacadeService<Department, string, DepartmentResource, DepartmentResource> DepartmentService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.DepartmentService = Substitute.For<IFacadeService<Department, string, DepartmentResource, DepartmentResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.DepartmentService);
                    with.Dependency<IResourceBuilder<Department>>(new DepartmentResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Department>>>(
                        new DepartmentsResourceBuilder());
                    with.Module<DepartmentsModule>();
                    with.ResponseProcessor<DepartmentsResponseProcessor>();
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