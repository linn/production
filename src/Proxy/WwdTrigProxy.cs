namespace Linn.Production.Proxy
{
    using Linn.Common.Proxy;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class WwdTrigProxy : IWwdTrigFunction
    {
        public int WwdTriggerRun(string partNumber, int qty)
        {
            var call = new OracleFunctionCall<int>("wwd_trig_run");
            call.AddParameter("p_part_number", partNumber, 14);
            call.AddParameter("p_qty", qty);
            return call.Execute();
        }
    }
}
