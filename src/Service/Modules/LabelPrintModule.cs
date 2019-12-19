namespace Linn.Production.Service.Modules
{
    using System;
    using Linn.Production.Facade;
    using Linn.Production.Service.Models;
    using Nancy;

    public sealed class LabelPrintModule : NancyModule
    {
        private readonly ILabelPrintService labelPrintService;

        public LabelPrintModule(ILabelPrintService labelPrintService)
        {
            this.labelPrintService = labelPrintService;

            this.Get("production/maintenance/labels/print", _ => this.GetApp());
            this.Post("production/maintenance/labels/print", _ => this.Print());

            this.Get("production/maintenance/labels/printers", _ => this.GetPrinters());
            this.Get("production/maintenance/labels/label-types", _ => this.GetLabelsTypes());
        }

        private object GetPrinters()
        {
            var result = this.labelPrintService.GetPrinters();
            return this.Negotiate.WithModel(result)
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
