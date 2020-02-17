namespace Linn.Production.Service.Modules
{
    using System;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class WorksOrderTimingsModule : NancyModule
    {

        private readonly IWorksOrderTimingsService worksOrderTimingsService;
        

        public WorksOrderTimingsModule(IWorksOrderTimingsService worksOrderTimingsService)
        {
            this.worksOrderTimingsService = worksOrderTimingsService;

            this.Get("/production/works-orders/timings", _ => this.GetWorksOrderTimingsForDates());
            this.Get("/production/works-orders/mw-timings-setup", _ => this.GetApp());
        }


        private object GetWorksOrderTimingsForDates()
        {
            var resource = this.Bind<SearchByDatesRequestResource>();
            //todo use new resource to take in two dates
            var worksOrders = this.worksOrderTimingsService.SearchByDates(resource.StartDate, resource.EndDate);

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
