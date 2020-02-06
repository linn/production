namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using Linn.Production.Domain.LinnApps.ATE;

    public interface ICountComponents
    {
        ComponentCount CountComponents(string part);
    }
}