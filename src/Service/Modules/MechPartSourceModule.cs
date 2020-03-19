namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class MechPartSourceModule : NancyModule
    {
        private readonly IFacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource> mechPartSourceFacadeService;

        public MechPartSourceModule(IFacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource> mechPartSourceFacadeService)
        {
            this.mechPartSourceFacadeService = mechPartSourceFacadeService;
            this.Get("/production/maintenance/mech-part-source", _ => this.GetMechPartSource());
            this.Get("/production/maintenance/mech-part-source/{id}", parameters => this.GetById(parameters.id));
            this.Put("/production/maintenance/mech-part-source/{id}", parameters => this.UpdateById(parameters.id));
        }

        private object UpdateById(int id)
        {
            var resource = this.Bind<MechPartSourceResource>();

            return this.Negotiate.WithModel(this.mechPartSourceFacadeService.Update(id, resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetById(int id)
        {
            return this.Negotiate.WithModel(this.mechPartSourceFacadeService.GetById(id))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetMechPartSource()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}