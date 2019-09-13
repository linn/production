namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    public interface IWorksOrderUtilities
    {
        void IssueSerialNumber(string partNumber, int orderNumber, string docType, int createdBy, int quantity);

        WorksOrderDetails GetWorksOrderDetails(string partNumber);
    }
}
