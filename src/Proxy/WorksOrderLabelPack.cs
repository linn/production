namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class WorksOrderLabelPack : IWorksOrderLabelPack
    {
        private readonly IDatabaseService databaseService;

        public WorksOrderLabelPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public void PrintLabels(int orderNumber, string printerGroup)
        {
            var connection = this.databaseService.GetConnection();

            var cmd = new OracleCommand("WO_LABEL_PACK.WO_LABELS", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            cmd.Parameters.Add(
                new OracleParameter("p_wo", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input,
                        Value = orderNumber
                    });

            cmd.Parameters.Add(
                new OracleParameter("p_part_number", OracleDbType.Varchar2)
                    {
                        Direction = ParameterDirection.Input,
                        Size = 2000,
                        Value = printerGroup
                    });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void PrintAioLabels(int orderNumber)
        {
            var connection = this.databaseService.GetConnection();

            var cmd = new OracleCommand("WO_LABEL_PACK.PRINT_WO_AIO_LABELS", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            cmd.Parameters.Add(
                new OracleParameter("p_wo", OracleDbType.Int32)
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
