namespace Linn.Production.Service.Modules
{
    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class LabelsModule : NancyModule
    {
        private readonly ILabelService labelService;

        public LabelsModule(ILabelService labelService)
        {
            this.labelService = labelService;
            this.Get("/production/maintenance/labels/reprint", _ => this.GetApp());
            this.Post("/production/maintenance/labels/reprint-mac-label/{serialNumber:int}", parameters => this.ReprintMACLabel(parameters.serialNumber));
            this.Post("/production/maintenance/labels/reprint-all/{serialNumber:int}", parameters => this.ReprintAllLabels(parameters.serialNumber));
        }

        private object ReprintMACLabel(int serialNumber)
        {
            try
            {
                this.labelService.PrintMACLabel(serialNumber);
            }
            catch (DomainException exception)
            {
                return this.Negotiate.WithModel(new BadRequestResult<Error>(exception.Message));
            }

            return HttpStatusCode.OK;
        }

        private object ReprintAllLabels(int serialNumber)
        {
            var resource = this.Bind<ArticleNumberRequestResource>();
            try
            {
                this.labelService.PrintAllLabels(serialNumber, resource.ArticleNumber);
            }
            catch (DomainException exception)
            {
                return this.Negotiate.WithModel(new BadRequestResult<Error>(exception.Message));
            }

            return HttpStatusCode.OK;
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}
