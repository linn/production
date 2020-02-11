namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class BuiltThisWeekModule : NancyModule
    {
        private readonly IBtwResultFacadeService btwResultFacadeService;

        public BuiltThisWeekModule(IBtwResultFacadeService btwResultFacadeService)
        {
            this.btwResultFacadeService = btwResultFacadeService;
            this.Get("/production/reports/btw", _ => this.GetBtwResult());
        }

        private object GetBtwResult()
        {
            var resource = this.Bind<CitCodeRequestResource>();

            var results = this.btwResultFacadeService.GenerateBtwResultForCit(
                resource.CitCode);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}