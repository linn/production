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
        private readonly IFacadeService<PartCadInfo, int, PartCadInfoResource, PartCadInfoResource> partCadInfoFacadeService;

        private readonly IAuthorisationService authorisationService;

        public PartCadInfoModule(
            IFacadeService<PartCadInfo, int, PartCadInfoResource, PartCadInfoResource> partCadInfoFacadeService,
            IAuthorisationService authorisationService)
        {
            this.partCadInfoFacadeService = partCadInfoFacadeService;
            this.authorisationService = authorisationService;
            this.Get("/production/maintenance/part-cad-info", _ => this.GetPartCadInfo());
            this.Get("/production/maintenance/part-cad-info/{id}", parameters => this.GetById(parameters.id));
            this.Put("/production/maintenance/part-cad-info/{id}", parameters => this.UpdateById(parameters.id));
        }

        private object UpdateById(int id)
        {
            var resource = this.Bind<PartCadInfoResource>();

            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.PartCadInfoUpdate, privileges))
            {
                return this.Negotiate.WithModel(this.partCadInfoFacadeService.Update(id, resource, privileges))
                    .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
            }

            return this.Negotiate.WithModel(
                new UnauthorisedResult<ResponseModel<PartCadInfo>>("You are not authorised to update Part Cad Info"));
        }

        private object GetById(int id)
        {
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            return this.Negotiate.WithModel(this.partCadInfoFacadeService.GetById(id, privileges))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetPartCadInfo()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}