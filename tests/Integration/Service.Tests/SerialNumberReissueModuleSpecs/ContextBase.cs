namespace Linn.Production.Service.Tests.SerialNumberReissueModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected ISerialNumberReissueService SerialNumberReissueService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.SerialNumberReissueService = Substitute.For<ISerialNumberReissueService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.SerialNumberReissueService);
                    with.Dependency<IResourceBuilder<SerialNumberReissue>>(new SerialNumberReissueResourceBuilder());
                    with.Module<SerialNumberReissueModule>();
                    with.ResponseProcessor<SerialNumberReissueResponseProcessor>();
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
