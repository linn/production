﻿namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;

    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;

    public sealed class CitsModule : NancyModule
    {
        private readonly IFacadeService<Cit, string, CitResource, CitResource> citService;

        public CitsModule(IFacadeService<Cit, string, CitResource, CitResource> citService)
        {
            this.citService = citService;

            this.Get("production/maintenance/cits", _ => this.GetCits());
        }


        private object GetCits()
        {
            var cits = this.citService.GetAll();

            return this.Negotiate.WithModel(cits).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
