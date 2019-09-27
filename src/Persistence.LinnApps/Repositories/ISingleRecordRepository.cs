namespace Linn.Production.Persistence.LinnApps.Repositories
{
    public interface ISingleRecordRepository<T>
    {
        T GetRecord();

        void UpdateRecord(T newValues);
    }
}