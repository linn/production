namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PartCadInfoModule : NancyModule
    {
        private readonly IFacadeService<PartCadInfo, int, PartCadInfoResource, PartCadInfoResource> partCadInfoFacadeService;

        public PartCadInfoModule(IFacadeService<PartCadInfo, int, PartCadInfoResource, PartCadInfoResource> partCadInfoFacadeService)
        {
            this.partCadInfoFacadeService = partCadInfoFacadeService;
            this.Get("/production/maintenance/part-cad-info", _ => this.GetPartCadInfo());
            this.Get("/production/maintenance/part-cad-info/{id}", parameters => this.GetById(parameters.id));
            this.Put("/production/maintenance/part-cad-info/{id}", parameters => this.UpdateById(parameters.id));
        }

        private object UpdateById(int id)
        {
            var resource = this.Bind<PartCadInfoResource>();

            return this.Negotiate.WithModel(this.partCadInfoFacadeService.Update(id, resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetById(int id)
        {
            return this.Negotiate.WithModel(this.partCadInfoFacadeService.GetById(id))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetPartCadInfo()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}