namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class SernosRenumPack : ISernosRenumPack
    {
        public string ReissueSerialNumber(
            string sernosGroup,
            int serialNumber,
            int? newSerialNumber,
            string articleNumber,
            string newArticleNumber,
            string comments,
            int? createdBy)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("SERNOS_RENUM_PACK.REISSUE_SERNOS", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var result = new OracleParameter(null, OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.ReturnValue,
                Size = 200
            };
            cmd.Parameters.Add(result);

            var sernosGroupParameter = new OracleParameter("p_sernos_group", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = 10,
                Value = sernosGroup
            };
            cmd.Parameters.Add(sernosGroupParameter);

            var serialNumberParameter = new OracleParameter("p_orig_serial_number", OracleDbType.Decimal)
            {
                Direction = ParameterDirection.Input,
                Value = serialNumber
            };
            cmd.Parameters.Add(serialNumberParameter);

            var newSerialNumberParameter = new OracleParameter("p_new_serial_number", OracleDbType.Decimal)
            {
                Direction = ParameterDirection.InputOutput,
                Value = newSerialNumber
            };
            cmd.Parameters.Add(newSerialNumberParameter);

            var articleNumberParameter = new OracleParameter("p_orig_article_number", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = 14,
                Value = articleNumber
            };
            cmd.Parameters.Add(articleNumberParameter);

            var newArticleNumberParameter = new OracleParameter("p_new_article_number", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = 14,
                Value = newArticleNumber
            };
            cmd.Parameters.Add(newArticleNumberParameter);

            var commentsParameter = new OracleParameter("p_comments", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = 200,
                Value = comments
            };
            cmd.Parameters.Add(commentsParameter);

            var createdByParameter = new OracleParameter("p_user_number", OracleDbType.Decimal)
            {
                Direction = ParameterDirection.Input,
                Value = createdBy
            };
            cmd.Parameters.Add(createdByParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return result.Value.ToString();
        }
    }
}