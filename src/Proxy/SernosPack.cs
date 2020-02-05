namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;
    using Oracle.ManagedDataAccess.Types;

    public class SernosPack : ISernosPack
    {
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

        public bool BuildSernos(
            int documentNumber,
            string docType,
            string partNumber,
            int docLine,
            int fromSerial,
            int toSerial,
            int userNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("SERNOS_PACK_V2.BUILD_SERNOS_WRAPPER", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var result = new OracleParameter(null, OracleDbType.Int32)
                             {
                                 Direction = ParameterDirection.ReturnValue
                             };
            cmd.Parameters.Add(result);

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

            var partNumberParameter = new OracleParameter("p_part_number", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Value = partNumber
            };
            cmd.Parameters.Add(partNumberParameter);

            var firstSernosParameter = new OracleParameter("p_first_sernos", OracleDbType.Int32)
            {
                Direction = ParameterDirection.Input,
                Size = 50,
                Value = fromSerial
            };
            cmd.Parameters.Add(firstSernosParameter);

            var lastSernosParameter = new OracleParameter("p_last_sernos", OracleDbType.Int32)
            {
                Direction = ParameterDirection.Input,
                Value = toSerial
            };
            cmd.Parameters.Add(lastSernosParameter);

            var raisedByParameter = new OracleParameter("p_raised_by", OracleDbType.Int32)
            {
                Direction = ParameterDirection.Input,
                Value = userNumber
            };
            cmd.Parameters.Add(raisedByParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return int.Parse(result.Value.ToString()) == 1;
        }

        public void ReIssueSernos(string originalPartNumber, string newPartNumber, int serialNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("SERNOS_PACK_V2.reissue_sernos", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new OracleParameter("p_orig_part_number", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input, Value = originalPartNumber, Size = 14
                                   });

            cmd.Parameters.Add(new OracleParameter("p_new_part_number", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Size = 14,
                                       Value = newPartNumber
                                   });

            cmd.Parameters.Add(new OracleParameter("p_serial_number", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.InputOutput,
                                       Value = serialNumber
                                   });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public string GetProductGroup(string partNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("SERNOS_PACK_V2.get_product_group", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Varchar2)
                             {
                                 Direction = ParameterDirection.ReturnValue,
                                 Size = 20
                             };
            cmd.Parameters.Add(result);

            cmd.Parameters.Add(new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Size = 50,
                                       Value = partNumber
                                   });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return result.Value?.ToString();
        }

        public void GetSerialNumberBoxes(string partNumber, out int numberOfSerialNumbers, out int numberOfBoxes)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("SERNOS_PACK_V2.GET_SERNOS_BOXES", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            cmd.Parameters.Add(new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Size = 50,
                                       Value = partNumber
                                   });
            var serialNumberQtyParameter = new OracleParameter(null, OracleDbType.Int32)
                                               {
                                                   Direction = ParameterDirection.InputOutput
                                               };
            cmd.Parameters.Add(serialNumberQtyParameter);
            var boxesQtyParameter = new OracleParameter(null, OracleDbType.Int32)
                                        {
                                            Direction = ParameterDirection.InputOutput
                                        };
            cmd.Parameters.Add(boxesQtyParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            numberOfSerialNumbers = serialNumberQtyParameter.Value.ToString() == "null" ? 0 : int.Parse(serialNumberQtyParameter.Value.ToString());
            numberOfBoxes = boxesQtyParameter.Value.ToString() == "null" ? 0 : int.Parse(boxesQtyParameter.Value.ToString());
        }

        public bool SerialNumberExists(int serialNumber, string partNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("SERNOS_PACK_V2.serial_number_exists_sql", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Int32)
                             {
                                 Direction = ParameterDirection.ReturnValue
                             };
            cmd.Parameters.Add(result);

            cmd.Parameters.Add(new OracleParameter("p_serial_number", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = serialNumber
                                   });

            cmd.Parameters.Add(new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Size = 50,
                                       Value = partNumber
                                   });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return result.Value.ToString() == "1";
        }

        public int GetNumberOfSernos(string partNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("SERNOS_PACK_V2.num_of_sernos", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Int32)
                             {
                                 Direction = ParameterDirection.ReturnValue
                             };
            cmd.Parameters.Add(result);



            cmd.Parameters.Add(new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Size = 50,
                                       Value = partNumber
                                   });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return ((OracleDecimal)result.Value).IsNull ? 0 : int.Parse(result.Value.ToString());
        }

        public string SernosMessage()
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("SERNOS_PACK_V2.SERNOS_MESS", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };
            var result = new OracleParameter(null, OracleDbType.Varchar2)
                             {
                                 Direction = ParameterDirection.ReturnValue,
                                 Size = 50
                             };
            cmd.Parameters.Add(result);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return result.Value.ToString();
        }
    }
}
