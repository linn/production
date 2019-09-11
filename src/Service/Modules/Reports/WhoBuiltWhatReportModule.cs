namespace Linn.Production.Service.Modules.Reports
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class WhoBuiltWhatReportModule : NancyModule
    {
        private readonly IWhoBuiltWhatReportFacadeService reportService;

        public WhoBuiltWhatReportModule(IWhoBuiltWhatReportFacadeService reportService)
        {
            this.reportService = reportService;
            this.Get("/production/reports/who-built-what/report", _ => this.WhoBuiltWhat());
            this.Get("/production/reports/who-built-what", _ => this.WhoBuiltWhatOptions());
            this.Get("/production/reports/who-built-what-details", _ => this.WhoBuiltWhatDetails());
        }

        private object WhoBuiltWhatOptions()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object WhoBuiltWhat()
        {
            var resource = this.Bind<WhoBuiltWhatRequestResource>();
            var results = this.reportService.WhoBuiltWhat(resource.FromDate, resource.ToDate, resource.CitCode);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object WhoBuiltWhatDetails()
        {
            var resource = this.Bind<WhoBuiltWhatRequestResource>();
            var results = this.reportService.WhoBuiltWhatDetails(resource.FromDate, resource.ToDate, resource.userNumber);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}                                                                              