namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    using Nancy;

    public class CitsModule : NancyModule
    {
        private readonly IFacadeService<Cit, string, CitResource, CitResource> citService;

        public CitsModule(IFacadeService<Cit, string, CitResource, CitResource> citService)
        {
            this.citService = citService;

            this.Get("/production/maintenance/cits", _ => this.GetCits());
        }

        private object GetCits()
        {
            return this.Negotiate.WithModel(this.citService.GetAll());
        }
    }
}
