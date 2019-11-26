namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface ISernosPack
    {
        bool SerialNumbersRequired(string partNumber);

        void IssueSernos(
            int documentNumber,
            string docType,
            int docLine,
            string partNumber,
            int createdBy,
            int quantity,
            int? firstSernosNumber);

        void ReIssueSernos(string originalPartNumber, string newPartNumber, int serialNumber);

        string GetProductGroup(string partNumber);
    }
}
