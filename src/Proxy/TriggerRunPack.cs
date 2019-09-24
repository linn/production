namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class TriggerRunPack : ITriggerRunPack
    {
        private readonly IDatabaseService databaseService;

        public TriggerRunPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public void AutoTriggerRun()
        {
            var connection = this.databaseService.GetConnection();

            var cmd = new OracleCommand("ST_PTL_PACK2_OO.AUTO_TRIGGER_RUN", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
