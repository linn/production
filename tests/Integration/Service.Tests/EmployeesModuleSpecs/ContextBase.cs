﻿namespace Linn.Production.Service.Tests.EmployeesModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;
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
        protected IFacadeService<Employee, int, EmployeeResource, EmployeeResource> EmployeeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.EmployeeService = Substitute.For<IFacadeService<Employee, int, EmployeeResource, EmployeeResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.EmployeeService);
                    with.Dependency<IResourceBuilder<Employee>>(new EmployeeResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Employee>>>(
                        new EmployeesResourceBuilder());
                    with.Module<EmployeesModule>();
                    with.ResponseProcessor<EmployeesResponseProcessor>();
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