namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;

    public class AssemblyFailsModule : NancyModule
    {
         private readonly IFacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource> facadeService;

        public AssemblyFailsModule(IFacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource> assemblyFailService)
        {
            this.facadeService = assemblyFailService;
            this.Get("/production/quality/assembly-fails/{id*}", parameters => this.GetById(parameters.id));
        }

        private object GetById(int id)
        {
            var result = this.facadeService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}