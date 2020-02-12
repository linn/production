namespace Linn.Production.Service.Modules
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
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
        

        public WorksOrderTimingsModule(IWorksOrdersService worksOrdersService)
        {
            this.worksOrderTimingsService = worksOrderTimingsService;

            this.Get("/production/works-order-timings", _ => this.GetWorksOrderTimingsForDates());
        }


        private object GetWorksOrderTimingsForDates()
        {
            var resource = this.Bind<SearchRequestResource>();
            //todo use new resource to take in two dates
            var worksOrders = this.worksOrderTimingsService.SearchByDates();

            return this.Negotiate.WithModel(worksOrders).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
