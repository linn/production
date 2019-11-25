namespace Linn.Production.Service.Modules
{
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.Responses;

    public sealed class HomeModule : NancyModule
    {
        public HomeModule()
        {
            this.Get("/", args => new RedirectResponse("/production/maintenance"));
            this.Get("/production", args => new RedirectResponse("/production/maintenance"));
            this.Get("/production/reports", args => new RedirectResponse("/production/maintenance"));
            this.Get("/production/resources", args => new RedirectResponse("/production/maintenance"));
            this.Get("/production/maintenance", _ => this.GetApp());
            this.Get("/production/reports/measures", _ => this.GetApp());

            this.Get("/production/quality/(.*)/create", _ => this.GetApp());
            this.Get("/production/reports/(.*)/create", _ => this.GetApp());
            this.Get("/production/resources/(.*)/create", _ => this.GetApp());
            this.Get("/production/maintenance/(.*)/create", _ => this.GetApp());
            this.Get("/production/(.*)/create", _ => this.GetApp());

            this.Get("/production/maintenance/signin-oidc-client", _ => this.GetApp());
            this.Get("/production/maintenance/signin-oidc-silent", _ => this.SilentRenew());

            this.Get(@"^(.*)$", _ => this.GetApp());
        }

        private object SilentRenew()
        {
            return this.Negotiate.WithView("silent-renew");
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}