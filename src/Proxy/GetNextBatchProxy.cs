namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class GetNextBatchProxy : IGetNextBatchService
    {
        private readonly IDatabaseService db;

        public GetNextBatchProxy(IDatabaseService db)
        {
            this.db = db;
        }

        public int GetNextBatch(string partNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("get_next_batch", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Int32) { Direction = ParameterDirection.ReturnValue };
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

            // TODO test if this actually works...
            return (int)result.Value;
        }
    }
}
