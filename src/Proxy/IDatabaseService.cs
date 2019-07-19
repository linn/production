namespace Linn.Production.Proxy
{
    using System.Data;

    public interface IDatabaseService
    {
        int GetIdSequence(string sequenceName);

        DataSet ExecuteQuery(string sql);
    }
}
