namespace Linn.Production.Proxy
{
    using System;
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;
    using Oracle.ManagedDataAccess.Types;

    public class LinnWeekPack : ILinnWeekPack
    {
        private readonly IDatabaseService db;

        public LinnWeekPack(IDatabaseService db)
        {
            this.db = db;
        }

        public string Wwsyy(DateTime date)
        {
            using (var connection = this.db.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("LINN_WEEK_PACK.WWSYY", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                     Size = 50
                                 };
                cmd.Parameters.Add(result);

                var dateParameter = new OracleParameter("p_date", OracleDbType.Date)
                                        {
                                            Direction = ParameterDirection.Input,
                                            Value = date
                                        };
                cmd.Parameters.Add(dateParameter);

                cmd.ExecuteNonQuery();
                connection.Close();
                return result.Value.ToString();
            }
        }

        public DateTime LinnWeekStartDate(int linnWeekNumber)
        {
            using (var connection = this.db.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("LINN_WEEK_PACK.linn_week_start_date", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Date)
                                 {
                                     Direction = ParameterDirection.ReturnValue
                                 };
                cmd.Parameters.Add(result);

                var linnWeekParameter = new OracleParameter("p_linn_week_number", OracleDbType.Int32)
                                            {
                                                Direction = ParameterDirection.Input,
                                                Value = linnWeekNumber
                                            };
                cmd.Parameters.Add(linnWeekParameter);

                cmd.ExecuteNonQuery();
                connection.Close();

                return ((OracleDate)result.Value).Value;
            }
        }

        public int LinnWeekNumber(DateTime date)
        {
            using (var connection = this.db.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("LINN_WEEK_PACK.linn_week_number", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Int32)
                                 {
                                     Direction = ParameterDirection.ReturnValue
                                 };
                cmd.Parameters.Add(result);

                var dateParameter = new OracleParameter("p_date", OracleDbType.Date)
                                        {
                                            Direction = ParameterDirection.Input,
                                            Value = date
                                        };
                cmd.Parameters.Add(dateParameter);

                cmd.ExecuteNonQuery();
                connection.Close();

                return int.Parse(result.Value.ToString());
            }
        }
    }
}
