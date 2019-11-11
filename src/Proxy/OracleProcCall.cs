namespace Linn.Production.Proxy
{
    using System.Data;
    using Oracle.ManagedDataAccess.Client;

    public class OracleProcCall
    {
        private OracleConnection connection;

        private OracleCommand cmd;

        public OracleProcCall(string functionName)
        {
            this.connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());
            this.cmd = new OracleCommand(functionName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
        }

        public void AddInputParameter(string paramName, string value, int size)
        {
            this.cmd.Parameters.Add(new OracleParameter(paramName, OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = size,
                Value = value
            });
        }

        public void AddInputParameter(string paramName, int value)
        {
            this.cmd.Parameters.Add(new OracleParameter(paramName, OracleDbType.Int32)
            {
                Direction = ParameterDirection.Input,
                Value = value
            });
        }

        public OracleParameter AddOutputParameterInt(string paramName, int size = 50)
        {
            var param = new OracleParameter(paramName, OracleDbType.Int32)
            {
                Direction = ParameterDirection.Output,
                Size = size
            };
            this.cmd.Parameters.Add(param);
            return param;
        }

        public void Execute()
        {
            this.connection.Open();
            this.cmd.ExecuteNonQuery();
            this.connection.Close();
        }
    }
}