namespace Linn.Production.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class ComponentCountResourceBuilder : IResourceBuilder<ComponentCount>
    {
        public object Build(ComponentCount model)
        {
            return new ComponentCountResource
                       {
                           SmtComponnets = model.SmtComponents,
                           PcbComponents = model.PcbComponents
                       };
        }

        public string GetLocation(ComponentCount model)
        {
            throw new System.NotImplementedException();
        }
    }
}