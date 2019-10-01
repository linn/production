namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface ISalesArticleService
    {
        string GetDescriptionFromPartNumber(string partNumber);

        bool ProductIdOnChip(string partNumber);
    }
}