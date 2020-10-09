namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class AssemblyFailsModule : NancyModule
    {
         private readonly IAssemblyFailsService assemblyFailService;

         private readonly
             IFacadeService<AssemblyFailFaultCode, string, AssemblyFailFaultCodeResource, AssemblyFailFaultCodeResource>
             faultCodeService;

         public AssemblyFailsModule(
             IAssemblyFailsService assemblyFailService,
             IFacadeService<AssemblyFailFaultCode, string, AssemblyFailFaultCodeResource, AssemblyFailFaultCodeResource> faultCodeService)
         {
             this.faultCodeService = faultCodeService;
             this.assemblyFailService = assemblyFailService;
             this.Get("/production/quality/assembly-fails/{id}", parameters => this.GetById(parameters.id));
             this.Post("/production/quality/assembly-fails", _ => this.Add());
             this.Get("/production/quality/assembly-fails", _ => this.Search());
             this.Get("/production/quality/assembly-fails/create", _ => this.GetApp());
             this.Put("/production/quality/assembly-fails/{id}", parameters => this.Update(parameters.id));
             this.Get("/production/quality/assembly-fail-fault-codes", _ => this.GetFaultCodes());
             this.Get("/production/quality/assembly-fail-fault-codes/create", _ => this.GetApp());
             this.Get(
                 "/production/quality/assembly-fail-fault-codes/{id*}",
                 parameters => this.GetFaultCode(parameters.id));
             this.Post("/production/quality/assembly-fail-fault-codes", _ => this.AddFaultCode());
             this.Put(
                 "/production/quality/assembly-fail-fault-codes/{id*}",
                 parameters => this.UpdateFaultCode(parameters.id));
         }

         private object GetById(int id)
        {
            var result = this.assemblyFailService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Add()
        {
            var resource = this.Bind<AssemblyFailResource>();
            var result = this.assemblyFailService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Update(int type)
        {
            var resource = this.Bind<AssemblyFailResource>();

            var result = this.assemblyFailService.Update(type, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Search()
        {
            var resource = this.Bind<AssemblyFailsSearchRequestResource>();

            if (resource.SearchTerm != null)
            {
                var result = this.assemblyFailService.Search(resource.SearchTerm);

                return this.Negotiate
                    .WithModel(result)
                    .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                    .WithView("Index");
            }
            
            var refinedResult = this.assemblyFailService.RefinedSearch(
                    resource.PartNumber,
                    resource.ProductId,
                    resource.Date,
                    resource.BoardPart,
                    resource.CircuitPart);
                return this.Negotiate
                    .WithModel(refinedResult)
                    .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                    .WithView("Index");
        }

        private object GetFaultCodes()
        {
            var result = this.faultCodeService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object GetFaultCode(string id)
        {
            var result = this.faultCodeService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddFaultCode()
        {
            var resource = this.Bind<AssemblyFailFaultCodeResource>();
            var result = this.faultCodeService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateFaultCode(string id)
        {
            var resource = this.Bind<AssemblyFailFaultCodeResource>();
            var result = this.faultCodeService.Update(id, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}