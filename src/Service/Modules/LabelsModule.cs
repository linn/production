namespace Linn.Production.Service.Modules
{
    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class LabelsModule : NancyModule
    {
        private readonly ILabelService labelService;

        private readonly IFacadeService<LabelReprint, int, LabelReprintResource, LabelReprintResource> labelReprintFacadeService;

        public LabelsModule(
            ILabelService labelService,
            IFacadeService<LabelReprint, int, LabelReprintResource, LabelReprintResource> labelReprintFacadeService)
        {
            this.labelService = labelService;
            this.labelReprintFacadeService = labelReprintFacadeService;
            this.Get("/production/maintenance/labels/reprint", _ => this.GetApp());
            this.Post("/production/maintenance/labels/reprint-mac-label", _ => this.ReprintMACLabel());
            this.Post("/production/maintenance/labels/reprint-all", _ => this.ReprintAllLabels());
            this.Get("/production/maintenance/labels/reprint-serial-number/{id:int}", parameters => this.GetReprintReIssue(parameters.id));
            this.Post("/production/maintenance/labels/reprint-serial-number", _ => this.ReprintReIssue());
        }

        private object GetReprintReIssue(int id)
        {
            var result = this.labelReprintFacadeService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object ReprintReIssue()
        {
            var resource = this.Bind<LabelReprintResource>();

            var result = this.labelReprintFacadeService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object ReprintMACLabel()
        {
            var resource = this.Bind<SerialNumberRequestResource>();

            try
            {
                this.labelService.PrintMACLabel(resource.SerialNumber);
            }
            catch (DomainException exception)
            {
                return this.Negotiate.WithModel(new BadRequestResult<Error>(exception.Message));
            }

            return HttpStatusCode.OK;
        }

        private object ReprintAllLabels()
        {
            var resource = this.Bind<ProductRequestResource>();

            try
            {
                this.labelService.PrintAllLabels(resource.SerialNumber, resource.ArticleNumber);
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
