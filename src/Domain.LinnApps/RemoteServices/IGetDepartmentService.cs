namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface IGetDepartmentService
    {
        string GetDepartment(string partNumber, string raisedByDepartment);
    }
}
