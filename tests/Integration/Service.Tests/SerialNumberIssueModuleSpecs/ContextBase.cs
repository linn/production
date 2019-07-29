namespace Linn.Production.Service.Tests.SerialNumberIssueModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Domain.LinnApps.SerialNumberIssue;
    using Facade.ResourceBuilders;
    using Resources;
    using Modules;
    using ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IFacadeService<SerialNumberIssue, int, SerialNumberIssueResource, SerialNumberIssueResource>
            SerialNumberIssueService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.SerialNumberIssueService = Substitute
                .For<IFacadeService<SerialNumberIssue, int, SerialNumberIssueResource, SerialNumberIssueResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.SerialNumberIssueService);
                    with.Dependency<IResourceBuilder<SerialNumberIssue>>(new SerialNumberIssueResourceBuilder());
                    with.Module<SerialNumberIssueModule>();
                    with.ResponseProcessor<SerialNumberIssueResponseProcessor>();
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
