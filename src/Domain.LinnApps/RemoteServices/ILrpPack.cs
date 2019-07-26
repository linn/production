namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface ILrpPack
    {
        decimal GetDaysToBuildPart(string partNumber, decimal quantity);
    }
}