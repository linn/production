namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class WorksOrderProxy : IWorksOrderProxyService
    {
        private readonly IDatabaseService db;

        public WorksOrderProxy(IDatabaseService db)
        {
            this.db = db;
        }

        public string CanRaiseWorksOrder(string partNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("CAN_RAISE_WORKS_ORDER_WRAPPER", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Varchar2)
                             {
                                 Direction = ParameterDirection.ReturnValue,
                                 Size = 2000
                             };
            cmd.Parameters.Add(result);

            var partNumberParameter = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Input,
                                              Size = 14,
                                              Value = partNumber
                                          };
            cmd.Parameters.Add(partNumberParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return result.Value.ToString();
        }

        public int GetNextBatch(string partNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("get_next_batch", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Int32)
                             {
                                 Direction = ParameterDirection.ReturnValue
                             };
            cmd.Parameters.Add(result);

            var partNumberParameter = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Input,
                                              Size = 14,
                                              Value = partNumber
                                          };
            cmd.Parameters.Add(partNumberParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return int.Parse(result.Value.ToString());
        }
    }
}
