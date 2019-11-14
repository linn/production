namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class WwdModule : NancyModule
    {
        private readonly IWwdResultFacadeService wwdResultFacadeService;

        public WwdModule(IWwdResultFacadeService wwdResultFacadeService)
        {
            this.wwdResultFacadeService = wwdResultFacadeService;
            this.Get("/production/reports/wwd", _ => this.GetWwdResult());
        }

        private object GetWwdResult()
        {
            var resource = this.Bind<WwdRequestResource>();

            var results =
                this.wwdResultFacadeService.GenerateWwdResultForTrigger(resource.PartNumber, resource.Qty,
                    resource.PtlJobref);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}