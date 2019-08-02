namespace Linn.Production.Proxy
{
    using System.Data;

    using Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class LrpPack : ILrpPack
    {
        private readonly IDatabaseService db;

        public LrpPack(IDatabaseService db)
        {
            this.db = db;
        }

        public decimal GetDaysToBuildPart(string partNumber, decimal quantity)
        {
            using (var connection = this.db.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("LRP_PACK.Days_To_Build_Part", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                     Size = 50
                                 };
                cmd.Parameters.Add(result);

                var partNumberParameter = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                              {
                                                  Direction = ParameterDirection.Input,
                                                  Size = 50,
                                                  Value = partNumber
                                              };
                cmd.Parameters.Add(partNumberParameter);

                var quantityParameter = new OracleParameter("p_quantity", OracleDbType.Decimal)
                                              {
                                                  Direction = ParameterDirection.Input,
                                                  Value = quantity
                                              };
                cmd.Parameters.Add(quantityParameter);

                cmd.ExecuteNonQuery();
                connection.Close();
                return new decimal(double.Parse(result.Value.ToString()));
            }  
        }
    }
}