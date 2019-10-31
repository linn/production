namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class AssemblyFailsModule : NancyModule
    {
         private readonly IFacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource> assemblyFailService;

         private readonly
             IFacadeService<AssemblyFailFaultCode, string, AssemblyFailFaultCodeResource, AssemblyFailFaultCodeResource>
             faultCodeService;

         public AssemblyFailsModule(
             IFacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource> assemblyFailService,
             IFacadeService<AssemblyFailFaultCode, string, AssemblyFailFaultCodeResource, AssemblyFailFaultCodeResource> faultCodeService)
         {
             this.faultCodeService = faultCodeService;
             this.assemblyFailService = assemblyFailService;
             this.Get("/production/quality/assembly-fails/{id}", parameters => this.GetById(parameters.id));
             this.Post("/production/quality/assembly-fails", _ => this.Add());
             this.Get("/production/quality/assembly-fails", _ => this.Search());
             this.Put("/production/quality/assembly-fails/{id}", parameters => this.Update(parameters.id));
             this.Get("/production/quality/assembly-fail-fault-codes", _ => this.GetFaultCodes());
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
            var resource = this.Bind<SearchRequestResource>();

            var result = this.assemblyFailService.Search(resource.SearchTerm);

            return this.Negotiate
                .WithModel(result)
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