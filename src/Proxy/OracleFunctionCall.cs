namespace Linn.Production.Proxy
{
    using System.Data;
    using Oracle.ManagedDataAccess.Client;

    public class OracleFunctionCall<T>
    {
        private OracleConnection connection;

        private OracleCommand cmd;

        private OracleParameter result;

        public OracleFunctionCall(string functionName)
        {
            this.connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());
            this.cmd = new OracleCommand(functionName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (typeof(T) == typeof(string))
            {
                this.result = new OracleParameter(null, OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.ReturnValue,
                    Size = 2000
                };
            }
            else if (typeof(T) == typeof(int))
            {
                this.result = new OracleParameter(null, OracleDbType.Int32)
                {
                    Direction = ParameterDirection.ReturnValue,
                };
            }

            this.cmd.Parameters.Add(result);
        }

        public void AddParameter(string paramName, string value, int size)
        {
            this.cmd.Parameters.Add(new OracleParameter(paramName, OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = size,
                Value = value
            });
        }

        public void AddParameter(string paramName, int value)
        {
            this.cmd.Parameters.Add(new OracleParameter(paramName, OracleDbType.Int32)
            {
                Direction = ParameterDirection.Input,
                Value = value
            });
        }

        public T Execute()
        {
            this.connection.Open();
            this.cmd.ExecuteNonQuery();
            this.connection.Close();

            return (T)result.Value;
        }
    }
}