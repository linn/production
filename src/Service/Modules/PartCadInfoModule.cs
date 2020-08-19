namespace Linn.Production.Service.Modules
{
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PartCadInfoModule : NancyModule
    {
        private readonly IFacadeService<Part, string, PartResource, PartResource> partsFacadeService;

        private readonly IAuthorisationService authorisationService;

        public PartCadInfoModule(
            IFacadeService<Part, string, PartResource, PartResource> partsFacadeService,
            IAuthorisationService authorisationService)
        {
            this.partsFacadeService = partsFacadeService;
            this.authorisationService = authorisationService;
            this.Get("/production/maintenance/part-cad-info", _ => this.GetPartCadInfo());
            this.Put("/production/maintenance/part-cad-info/{id}", parameters => this.UpdatePart(parameters.id));
        }

        private object UpdatePart(string id)
        {
            var resource = this.Bind<PartResource>();

            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.PartCadInfoUpdate, privileges))
            {
                return this.Negotiate.WithModel(this.partsFacadeService.Update(id, resource, privileges))
                    .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
            }

            return this.Negotiate.WithModel(
                new UnauthorisedResult<ResponseModel<Part>>("You are not authorised to update Part Cad Info"));
        }

        private object GetPartCadInfo()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}