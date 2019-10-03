namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Models;

    using Nancy;

    public sealed class StoragePlacesModule : NancyModule
    {
        private readonly IStoragePlaceService storagePlaceService;

        public StoragePlacesModule(IStoragePlaceService storagePlaceService)
        {
            this.storagePlaceService = storagePlaceService;
            this.Get("production/maintenance/storage-places", _ => this.GetStoragePlaces());
        }

        private object GetStoragePlaces()
        {
            var parts = this.storagePlaceService.GetAll();

            return this.Negotiate.WithModel(parts).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}