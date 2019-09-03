namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class CanRaiseWorksOrderProxy : ICanRaiseWorksOrderService
    {
        private readonly IDatabaseService db;

        public CanRaiseWorksOrderProxy(IDatabaseService db)
        {
            this.db = db;
        }

        public string CanRaiseWorksOrder(string partNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("can_raise_works_order", connection)
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

            var messageParameter = new OracleParameter("p_message", OracleDbType.Varchar2)
                                       {
                                           Direction = ParameterDirection.Output,
                                           Size = 2000
                                       };
            cmd.Parameters.Add(messageParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            var success = int.Parse(result.Value.ToString());

            return success == 1 ? "SUCCESS" : messageParameter.Value.ToString();
        }
    }
}
