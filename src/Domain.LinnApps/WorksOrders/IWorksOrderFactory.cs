namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    public interface IWorksOrderFactory
    {
        WorksOrder RaiseWorksOrder(WorksOrder worksOrder);

        void IssueSerialNumber(string partNumber, int orderNumber, string docType, int createdBy, int quantity);

        WorksOrder CancelWorksOrder(WorksOrder worksOrder, int? cancelledBy, string reasonCancelled);

        WorksOrderDetails GetWorksOrderDetails(string partNumber);
    }
}
