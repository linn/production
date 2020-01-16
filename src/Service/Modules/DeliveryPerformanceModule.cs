namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class DeliveryPerformanceModule : NancyModule
    {
        private readonly IDeliveryPerfResultFacadeService deliveryPerfResultFacadeService;

        public DeliveryPerformanceModule(IDeliveryPerfResultFacadeService deliveryPerfResultFacadeService)
        {
            this.deliveryPerfResultFacadeService = deliveryPerfResultFacadeService;
            this.Get("/production/reports/delperf", _ => this.GetBtwResult());
        }

        private object GetBtwResult()
        {
            var resource = this.Bind<CitCodeRequestResource>();

            var results = this.deliveryPerfResultFacadeService.GenerateDelPerfSummaryForCit(
                resource.CitCode);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}