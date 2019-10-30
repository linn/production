namespace Linn.Production.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Resources;

    public class ProductionTriggerAssemblyResourceBuilder : IResourceBuilder<ProductionTriggerAssembly>
    {
        public object Build(ProductionTriggerAssembly assembly)
        {
            return this.BuildAssembly(assembly);
        }

        public ProductionTriggerAssemblyResource BuildAssembly(ProductionTriggerAssembly assembly)
        {
            return new ProductionTriggerAssemblyResource
            {
                PartNumber = assembly.PartNumber,
                AssemblyNumber = assembly.AssemblyNumber,
                BomLevel = assembly.BomLevel,
                QtyBeingBuilt = assembly.QtyBeingBuilt,
                ReqtForInternalAndTriggerLevelBT = assembly.ReqtForInternalAndTriggerLevelBT,
                Jobref = assembly.Jobref,
                NettSalesOrders = assembly.NettSalesOrders,
                QtyUsed = assembly.QtyUsed,
                RemainingBuild = assembly.RemainingBuild,
                ReqtForPriorityBuildBE = assembly.ReqtForPriorityBuildBE
            };
        }

        public string GetLocation(ProductionTriggerAssembly model)
        {
            throw new System.NotImplementedException();
        }
    }
}