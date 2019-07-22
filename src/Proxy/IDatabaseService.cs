namespace Linn.Production.Proxy
{
    using System.Data;

    using Oracle.ManagedDataAccess.Client;

    public interface IDatabaseService
    {
        OracleConnection GetConnection();
    }
}