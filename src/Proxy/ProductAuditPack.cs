namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class ProductAuditPack : IProductAuditPack
    {
        public void GenerateProductAudits(int orderNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("PRODUCT_AUDIT_PACK.GENERATE_PRODUCT_AUDITS", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var worksOrderNumberParameter = new OracleParameter("p_works_order_number", OracleDbType.Decimal)
                             {
                                 Direction = ParameterDirection.Input,
                                 Size = 200
                             };
            cmd.Parameters.Add(worksOrderNumberParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
