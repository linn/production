namespace Linn.Production.Service.Modules
{
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class MetalWorkTimingsModule : NancyModule
    {
        private readonly IMetalWorkTimingsService metalWorkTimingsService;

        public MetalWorkTimingsModule(IMetalWorkTimingsService metalWorkTimingsService)
        {
            this.metalWorkTimingsService = metalWorkTimingsService;

            this.Get("/production/reports/mw-timings", _ => this.GetWorksOrderTimingsForDates());
        }

        private object GetWorksOrderTimingsForDates()
        {
            var resource = this.Bind<SearchByDatesRequestResource>();
            //todo use new resource to take in two dates
            var worksOrders = this.metalWorkTimingsService.GetMetalWorkTimingsReport(resource.StartDate, resource.EndDate);

            return this.Negotiate.WithModel(worksOrders).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetApp()
        {
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            return this.Negotiate
                .WithModel(new SuccessResult<ResponseModel<WorksOrderTiming>>(new ResponseModel<WorksOrderTiming>(new WorksOrderTiming(), privileges)))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
