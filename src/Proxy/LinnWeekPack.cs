namespace Linn.Production.Proxy
{
    using System;
    using System.Data;

    using Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;
    using Oracle.ManagedDataAccess.Types;

    public class LinnWeekPack : ILinnWeekPack
    {
        private readonly IDatabaseService db;

        public LinnWeekPack(IDatabaseService db)
        {
            this.db = db;
        }

        public DateTime GetLinnWeekEndDate(DateTime date)
        {
            using (var connection = this.db.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("LINN_WEEK_PACK.LINN_WEEK_END_DATE", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Date)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                 };
                cmd.Parameters.Add(result);

                var dateParameter = new OracleParameter("p_part_number", OracleDbType.Date)
                                              {
                                                  Direction = ParameterDirection.Input,
                                                  Value = date
                                              };
                cmd.Parameters.Add(dateParameter);

                cmd.ExecuteNonQuery();
                connection.Close();
                return ((OracleDate)result.Value).Value;
            }
        }
    }
}
