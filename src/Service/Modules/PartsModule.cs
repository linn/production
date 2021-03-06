﻿namespace Linn.Production.Service.Modules
{
    using System;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PartsModule : NancyModule
    {
        private readonly IFacadeService<Part, string, PartResource, PartResource> partsFacadeService;

        public PartsModule(
            IFacadeService<Part, string, PartResource, PartResource> partsFacadeService)
        {
            this.partsFacadeService = partsFacadeService;
            this.Get("/production/maintenance/parts", _ => this.GetParts());
            this.Get("/production/maintenance/parts/{id}", parameters => this.GetPartById(parameters.id));
        }

        private object GetPartById(string id)
        {
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            return this.Negotiate.WithModel(this.partsFacadeService.GetById(id, privileges))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetParts()
        {
            var resource = this.Bind<SearchRequestResource>();

            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            var results = string.IsNullOrEmpty(resource.SearchTerm)
                              ? this.partsFacadeService.GetAll(privileges)
                              : this.partsFacadeService.Search(resource.SearchTerm, privileges);
            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}