namespace Linn.Production.Proxy
{
    using System.Data;

    using Oracle.ManagedDataAccess.Client;

    public class DatabaseService : IDatabaseService
    {
        public OracleConnection GetConnection()
        {
            return new OracleConnection(ConnectionStrings.ManagedConnectionString());
        }
    }
}
