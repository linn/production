namespace Linn.Production.Proxy
{
    using System;

    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class CountComponentsService : ICountComponents
    {
        public ComponentCount CountComponents(string part)
        {
            var proc = new OracleProcCall("count_components");
            proc.AddInputParameter("p_part_number", part, 14);
            var result1 = proc.AddOutputParameterInt("p_smt_components");
            var result2 = proc.AddOutputParameterInt("p_pcb_components");

            proc.Execute();

            return new ComponentCount {
                                            PcbComponents = Convert.ToInt32(result1.Value.ToString()),
                                            SmtComponents = Convert.ToInt32(result2.Value.ToString())
                                        };
        }
    }
}