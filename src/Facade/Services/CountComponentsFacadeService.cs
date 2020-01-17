namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class CountComponentsFacadeService : ICountComponentsFacadeService
    {
        private readonly ICountComponents countComponentsService;

        public CountComponentsFacadeService(ICountComponents countComponentsService)
        {
            this.countComponentsService = countComponentsService;
        }

        public SuccessResult<ComponentCount> CountComponents(string part)
        {
            return new SuccessResult<ComponentCount>(this.countComponentsService.CountComponents(part));
        }
    }
}