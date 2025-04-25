namespace Linn.Production.Service.Tests.ManufacturingSkillsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    { 
        protected IFacadeFilterService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource, ManufacturingSkillsRequestResource> ManufacturingSkillFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ManufacturingSkillFacadeService = Substitute.For<IFacadeFilterService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource, ManufacturingSkillsRequestResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ManufacturingSkillFacadeService);
                    with.Dependency<IResourceBuilder<ManufacturingSkill>>(new ManufacturingSkillResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<ManufacturingSkill>>>(
                        new ManufacturingSkillsResourceBuilder());
                    with.Module<ManufacturingSkillsModule>();
                    with.ResponseProcessor<ManufacturingSkillsResponseProcessor>();
                    with.ResponseProcessor<ManufacturingSkillResponseProcessor>();
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
