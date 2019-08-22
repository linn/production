namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;

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
             this.Get("/production/quality/assembly-fails/{id*}", parameters => this.GetById(parameters.id));
             this.Get("/production/quality/assembly-fail-fault-codes", _ => this.GetFaultCodes());
         }

        private object GetById(int id)
        {
            var result = this.assemblyFailService.GetById(id);
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
    }
}