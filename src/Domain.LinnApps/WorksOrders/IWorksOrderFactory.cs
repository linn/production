namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    public interface IWorksOrderFactory
    {
        WorksOrder RaiseWorksOrder(string partNumber, string raisedByDepartment, int raisedBy);

        void IssueSerialNumber(string partNumber, int orderNumber, string docType, int createdBy, int quantity);

        WorksOrder CancelWorksOrder(WorksOrder worksOrder, int? cancelledBy, string reasonCancelled);
    }
}
