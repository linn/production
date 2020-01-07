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
       
        private object GetApp()
        {
            return this.Negotiate
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
