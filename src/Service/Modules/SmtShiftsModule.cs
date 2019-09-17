namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;

    public sealed class SmtShiftsModule : NancyModule
    {
        private readonly IFacadeService<SmtShift, string, SmtShiftResource, SmtShiftResource> citService;

        public SmtShiftsModule(IFacadeService<SmtShift, string, SmtShiftResource, SmtShiftResource> citService)
        {
            this.citService = citService;

            this.Get("production/maintenance/shifts", _ => this.GetSmtShifts());
        }


        private object GetSmtShifts()
        {
            var cits = this.citService.GetAll();

            return this.Negotiate.WithModel(cits).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}