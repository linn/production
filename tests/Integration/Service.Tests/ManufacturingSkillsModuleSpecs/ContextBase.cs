using Linn.Production.Domain.LinnApps;

namespace Linn.Production.Service.Tests.ManufacturingSkillsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
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
        protected IFacadeService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource> ManufacturingSkillService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ManufacturingSkillService = Substitute.For<IFacadeService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ManufacturingSkillService);
                    with.Dependency<IResourceBuilder<ManufacturingSkill>>(new ManufacturingSkillResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<ManufacturingSkill>>>(
                        new ManufacturingSkillsResourceBuilder());
                    with.Module<ManufacturingSkillsModule>();
                    with.ResponseProcessor<ManufacturingSkillsResponseProcessor>();
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