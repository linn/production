namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class BartenderLabelPack : IBartenderLabelPack
    {
        public bool PrintLabels(
            string fileName,
            string printer,
            int qty,
            string template,
            string data,
            ref string message)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("BARTENDER.PRINT_LABELS_WRAPPER", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Int32)
                             {
                                 Direction = ParameterDirection.ReturnValue,
                                 Size = 2000
                             };
            cmd.Parameters.Add(result);

            cmd.Parameters.Add(new OracleParameter("p_filename", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Size = 50,
                                       Value = fileName
                                   });
            cmd.Parameters.Add(new OracleParameter("p_printer", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Size = 100,
                                       Value = printer
                                   });
            cmd.Parameters.Add(new OracleParameter("p_qty", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = qty
                                   });
            cmd.Parameters.Add(new OracleParameter("p_template", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Size = 100,
                                       Value = template
                                   });
            cmd.Parameters.Add(new OracleParameter("p_data", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Size = 4000,
                                       Value = data
                                   });
            cmd.Parameters.Add(new OracleParameter("p_message", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.InputOutput,
                                       Size = 500,
                                       Value = message
                                   });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return result.Value.ToString() == "1";
        }

        public void WorksOrderLabels(int orderNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("BARTENDER.WO_LABELS", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            cmd.Parameters.Add(new OracleParameter("p_wo", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = orderNumber
                                   });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
