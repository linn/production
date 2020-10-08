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
            this.Get("/production/reports/delperf", _ => this.GetDeliveryPerfResult());
            this.Get("/production/reports/delperf/details", _ => this.GetDeliveryPerfDetails());
        }

        private object GetDeliveryPerfResult()
        {
            var resource = this.Bind<CitCodeRequestResource>();

            var results = this.deliveryPerfResultFacadeService.GenerateDelPerfSummaryForCit(
                resource.CitCode);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetDeliveryPerfDetails()
        {
            var resource = this.Bind<DelPerfDetailRequestResource>();

            var results = this.deliveryPerfResultFacadeService.GetDelPerfDetail(
                resource.CitCode, resource.Priority);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}