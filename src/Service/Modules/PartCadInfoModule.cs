namespace Linn.Production.Service.Modules
{
    using Linn.Production.Service.Models;

    using Nancy;

    public sealed class PartCadInfoModule : NancyModule
    {
        public PartCadInfoModule()
        {
            this.Get("/production/maintenance/part-cad-info", _ => this.GetPartCadInfo());
        }

        private object GetPartCadInfo()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}