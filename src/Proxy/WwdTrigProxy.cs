﻿namespace Linn.Production.Proxy
{
    using System;
    using System.Data;
    using Linn.Common.Proxy;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Oracle.ManagedDataAccess.Client;

    public class WwdTrigProxy : IWwdTrigFunction
    {
        public int WwdTriggerRun(string partNumber, int qty)
        {
            var proc = new OracleProcCall("wwd_trig_run");
            proc.AddInputParameter("p_part_number", partNumber, 14);
            proc.AddInputParameter("p_qty", qty);
            var result = proc.AddOutputParameterInt("p_job_uid");
            proc.Execute();

            return Convert.ToInt32(result.Value.ToString());
        }
    }
}
