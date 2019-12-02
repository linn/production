namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class LabelPack : ILabelPack
    {
        public string GetLabelData(string labelTypeCode, int? serialNumber, string articleNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("GET_LABEL_DATA", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Varchar2)
                             {
                                 Direction = ParameterDirection.ReturnValue,
                                 Size = 4000
                             };
            cmd.Parameters.Add(result);

            cmd.Parameters.Add(new OracleParameter("p_lt_code", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = labelTypeCode,
                                       Size = 30
                                   });

            cmd.Parameters.Add(new OracleParameter("p_product_id", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = serialNumber
                                   });

            cmd.Parameters.Add(new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = articleNumber,
                                       Size = 14
                                   });

            cmd.Parameters.Add(new OracleParameter("p_label_type", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = string.Empty,
                                       Size = 30
                                   });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return result.Value.ToString();
        }
    }
}
