namespace Linn.Production.Service.Tests.WwdModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IWwdResultFacadeService WwdResultFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.WwdResultFacadeService = Substitute.For<IWwdResultFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.WwdResultFacadeService);
                    with.Dependency<IResourceBuilder<WwdResult>>(new WwdResultResourceBuilder());
                    with.Module<WwdModule>();
                    with.ResponseProcessor<WwdResultReportResponseProcessor>();
                });

            this.Browser = new Browser(bootstrapper);
        }
    }
}
