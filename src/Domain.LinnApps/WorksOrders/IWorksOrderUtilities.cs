namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    public interface IWorksOrderUtilities
    {
        void IssueSerialNumber(string partNumber, int orderNumber, string docType, int createdBy, int quantity);

        WorksOrderPartDetails GetWorksOrderDetails(string partNumber);

        Department GetDepartment(string partNumber);

        int GetNextLabelSeqForPart(string partNumber);
    }
}
