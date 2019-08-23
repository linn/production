namespace Linn.Production.Persistence.LinnApps.Repositories
{
    public interface IMasterRepository<T>
    {
        T GetMasterRecord();
    }
}