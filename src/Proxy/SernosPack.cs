namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class SernosPack : ISernosPack
    {
        private readonly IDatabaseService db;

        public SernosPack(IDatabaseService db)
        {
            this.db = db;
        }

        public bool SerialNumbersRequired(string partNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("SERNOS_PACK_V2.SERIAL_NOS_REQD_WRAPPER", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Varchar2)
                             {
                                 Direction = ParameterDirection.ReturnValue,
                                 Size = 2000
                             };
            cmd.Parameters.Add(result);

            var partNumberParameter = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Input,
                                              Size = 50,
                                              Value = partNumber
                                          };
            cmd.Parameters.Add(partNumberParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return result.Value.ToString() == "SUCCESS";
        }

        public void IssueSernos(
            int documentNumber,
            string docType,
            int docLine,
            string partNumber,
            int createdBy,
            int quantity,
            int? firstSernosNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("SERNOS_PACK_V2.ISSUE_SERNOS", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var documentNumberParameter = new OracleParameter("p_document_number", OracleDbType.Int32)
                                              {
                                                  Direction = ParameterDirection.Input,
                                                  Value = documentNumber
                                              };
            cmd.Parameters.Add(documentNumberParameter);

            var documentTypeParameter = new OracleParameter("p_doc_type", OracleDbType.Varchar2)
                                            {
                                                Direction = ParameterDirection.Input,
                                                Value = docType,
                                                Size = 2
                                            };
            cmd.Parameters.Add(documentTypeParameter);

            var docLineParameter = new OracleParameter("p_doc_line", OracleDbType.Int32)
                                       {
                                           Direction = ParameterDirection.Input,
                                           Value = docLine
                                       };
            cmd.Parameters.Add(docLineParameter);

            var partNumberParameter = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Input,
                                              Size = 50,
                                              Value = partNumber
                                          };
            cmd.Parameters.Add(partNumberParameter);

            var raisedByParameter = new OracleParameter("p_raised_by", OracleDbType.Int32)
                                        {
                                            Direction = ParameterDirection.Input,
                                            Value = createdBy
                                        };
            cmd.Parameters.Add(raisedByParameter);

            var quantityParameter = new OracleParameter("p_qty", OracleDbType.Int32)
                                        {
                                            Direction = ParameterDirection.Input,
                                            Value = quantity
                                        };
            cmd.Parameters.Add(quantityParameter);

            var firstSernosNumberParameter = new OracleParameter("p_first_sernos_number", OracleDbType.Int32)
                                        {
                                            Direction = ParameterDirection.InputOutput,
                                            Value = firstSernosNumber
                                        };
            cmd.Parameters.Add(firstSernosNumberParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
