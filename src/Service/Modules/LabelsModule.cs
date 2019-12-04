namespace Linn.Production.Service.Modules
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class LabelsModule : NancyModule
    {
        private readonly ILabelService labelService;

        private readonly IFacadeService<LabelReprint, int, LabelReprintResource, LabelReprintResource> labelReprintFacadeService;

        private readonly IAuthorisationService authorisationService;

        public LabelsModule(
            ILabelService labelService,
            IFacadeService<LabelReprint, int, LabelReprintResource, LabelReprintResource> labelReprintFacadeService,
            IAuthorisationService authorisationService)
        {
            this.labelService = labelService;
            this.labelReprintFacadeService = labelReprintFacadeService;
            this.authorisationService = authorisationService;
            this.Get("/production/maintenance/labels/reprint", _ => this.GetApp());
            this.Post("/production/maintenance/labels/reprint-mac-label", _ => this.ReprintMACLabel());
            this.Post("/production/maintenance/labels/reprint-all", _ => this.ReprintAllLabels());
            this.Get("/production/maintenance/labels/reprint-reasons/application-state", _ => this.GetApp());
            this.Get("/production/maintenance/labels/reprint-reasons/create", _ => this.GetApp());
            this.Get("/production/maintenance/labels/reprint-reasons/{id:int}", parameters => this.GetReprintReIssue(parameters.id));
            this.Post("/production/maintenance/labels/reprint-reasons", _ => this.ReprintReIssue());
        }

        private object GetReprintReIssue(int id)
        {
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            var result = this.labelReprintFacadeService.GetById(id, privileges);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object ReprintReIssue()
        {
            var resource = this.Bind<LabelReprintResource>();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            if (!this.authorisationService.HasPermissionFor("serial-number.reissue", privileges) && resource.ReprintType != "REPRINT")
            {
                return this.Negotiate.WithModel(new UnauthorisedResult<ResponseModel<LabelReprint>>("You are not authorised to reissue or rebuild serial numbers"));
            }

            resource.Links = new[] { new LinkResource("requested-by", this.Context?.CurrentUser?.GetEmployeeUri()) };
            var result = this.labelReprintFacadeService.Add(resource, privileges);
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
            return this.Negotiate
                .WithModel(new SuccessResult<ResponseModel<LabelReprint>>(new ResponseModel<LabelReprint>(new LabelReprint(), new List<string>())))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
