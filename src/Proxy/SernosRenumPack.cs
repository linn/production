namespace Linn.Production.Proxy
{
    using System.Data;

    using Domain.LinnApps.RemoteServices;

    using Linn.Production.Resources;

    using Oracle.ManagedDataAccess.Client;

    public class SernosRenumPack : ISernosRenumPack
    {
        public string ReissueSerialNumber(SerialNumberReissueResource resource)
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
                Value = resource.SernosGroup
            };
            cmd.Parameters.Add(sernosGroupParameter);

            var serialNumberParameter = new OracleParameter("p_orig_serial_number", OracleDbType.Decimal)
            {
                Direction = ParameterDirection.Input,
                Value = resource.SerialNumber
            };
            cmd.Parameters.Add(serialNumberParameter);

            var newSerialNumberParameter = new OracleParameter("p_new_serial_number", OracleDbType.Decimal)
            {
                Direction = ParameterDirection.InputOutput,
                Value = resource.NewSerialNumber
            };
            cmd.Parameters.Add(newSerialNumberParameter);

            var articleNumberParameter = new OracleParameter("p_orig_article_number", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = 14,
                Value = resource.ArticleNumber
            };
            cmd.Parameters.Add(articleNumberParameter);

            var newArticleNumberParameter = new OracleParameter("p_new_article_number", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = 14,
                Value = resource.NewArticleNumber
            };
            cmd.Parameters.Add(newArticleNumberParameter);

            var commentsParameter = new OracleParameter("p_comments", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = 200,
                Value = resource.Comments
            };
            cmd.Parameters.Add(commentsParameter);

            var createdByParameter = new OracleParameter("p_user_number", OracleDbType.Decimal)
            {
                Direction = ParameterDirection.Input,
                Value = resource.CreatedBy
            };
            cmd.Parameters.Add(createdByParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return result.Value.ToString();
        }
    }
}