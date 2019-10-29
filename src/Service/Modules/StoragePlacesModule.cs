namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

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
            var resource = this.Bind<SearchRequestResource>();

            var locations = this.storagePlaceService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(locations).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}