namespace Linn.Production.Service.Modules
{
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.Responses;

    public sealed class HomeModule : NancyModule
    {
        public HomeModule()
        {
            this.Get("/", args => new RedirectResponse("/production"));
            this.Get("/production", _ => this.GetApp());
            this.Get("/production/(.*)/create", _ => this.GetApp());

            this.Get("/production/signin-oidc-client", _ => this.GetApp());
            this.Get("/production/signin-oidc-silent", _ => this.SilentRenew());
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