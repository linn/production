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

        public DataSet ExecuteQuery(string sql)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionStrings.ManagedConnectionString()))
            {
                var dataAdapter = new OracleDataAdapter(
                    new OracleCommand(sql, connection) { CommandType = CommandType.Text });
                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }

        public int GetIdSequence(string sequenceName)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("get_next_sequence_value", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var result = new OracleParameter(string.Empty, OracleDbType.Int32)
            {
                Direction = ParameterDirection.ReturnValue
            };
            cmd.Parameters.Add(result);

            var sequenceParameter = new OracleParameter("p_sequence", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = 50,
                Value = sequenceName
            };
            cmd.Parameters.Add(sequenceParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return int.Parse(result.Value?.ToString());
        }
    }
}
