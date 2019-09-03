﻿namespace Linn.Production.Proxy
{
    using System.Data;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    using Oracle.ManagedDataAccess.Client;

    public class GetDepartmentProxy : IGetDepartmentService
    {
        private readonly IDatabaseService db;

        public GetDepartmentProxy(IDatabaseService db)
        {
            this.db = db;
        }

        public string GetDepartment(string partNumber, string raisedByDepartment)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("get_department", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Int32)
                             {
                                 Direction = ParameterDirection.ReturnValue
                             };
            cmd.Parameters.Add(result);

            var partNumberParameter = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.Input,
                                     Size = 14,
                                     Value = partNumber
                                 };
            cmd.Parameters.Add(partNumberParameter);

            var departmentCodeParameter = new OracleParameter("p_dept_code", OracleDbType.Varchar2)
                                              {
                                                  Direction = ParameterDirection.InputOutput,
                                                  Size = 10,
                                                  Value = raisedByDepartment
                                              };
            cmd.Parameters.Add(departmentCodeParameter);

            // TODO how does this work? Not sure where it was coming from in form...
            var departmentDescriptionParameter = new OracleParameter("p_dept_desc", OracleDbType.Varchar2)
                                                     {
                                                         Direction = ParameterDirection.InputOutput,
                                                         Size = 50
                                                     };
            cmd.Parameters.Add(departmentDescriptionParameter);
                
            var messageParameter = new OracleParameter("p_message", OracleDbType.Varchar2)
                                       {
                                           Direction = ParameterDirection.Output,
                                           Size = 2000
                                       };
            cmd.Parameters.Add(messageParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            var success = int.Parse(result.Value.ToString());

            return success == 1 ? "SUCCESS" : messageParameter.Value.ToString();
        }
    }
}
