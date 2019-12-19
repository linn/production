namespace Linn.Production.Service.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Dispatchers;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class LabelPrintModule : NancyModule
    {
        private readonly ILabelPrintService labelPrintService;
        private readonly IAuthorisationService authorisationService;

        public LabelPrintModule(
           ILabelPrintService labelPrintService,
            IAuthorisationService authorisationService)
        {
            this.labelPrintService = labelPrintService;
            this.authorisationService = authorisationService;

            this.Get("production/maintenance/labels/print", _ => this.GetApp());
            this.Post("production/maintenance/labels/print", _ => this.Print());

            this.Get("production/maintenance/labels/printers", _ => this.GetPrinters());
            this.Get("production/maintenance/labels/label-types", _ => this.GetLabelsTypes());
        }

        private object GetPrinters()
        {
            return this.Negotiate.WithModel(this.labelPrintService.GetPrinters())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetLabelsTypes()
        {
            return this.Negotiate.WithModel(this.labelPrintService.GetLabelTypes())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Print()
        {
            throw new NotImplementedException();
            //return this.Negotiate.WithModel(this.labelPrintService.GetLabelTypes())
            //    .WithMediaRangeModel("text/html", ApplicationSettings.Get)
            //    .WithView("Index");
        }

        //private object GetLabelPrint()
        //{
        //    var resource = this.Bind<LabelPrintSearchRequestResource>();

        //    this.RequiresAuthentication();
        //    var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            
        //    IResult<ResponseModel<IEnumerable<ProductionTriggerLevel>>> parts;

        //    if (!string.IsNullOrWhiteSpace(resource.SearchTerm)
        //        || !string.IsNullOrWhiteSpace(resource.CitSearchTerm)
        //        || (!string.IsNullOrWhiteSpace(resource.OverrideSearchTerm) && resource.OverrideSearchTerm != "null")
        //        || (!string.IsNullOrWhiteSpace(resource.AutoSearchTerm) && resource.AutoSearchTerm != "null"))
        //    {
        //        parts = this.labelPrintService.Search(resource, privileges);
        //    }
        //    else
        //    {
        //        parts = this.labelPrintService.GetAll(privileges);
        //    }

        //    return this.Negotiate.WithModel(parts).WithMediaRangeModel("text/html", ApplicationSettings.Get)
        //        .WithView("Index");
        //}
        private object GetApp()
        {
            return this.Negotiate
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
