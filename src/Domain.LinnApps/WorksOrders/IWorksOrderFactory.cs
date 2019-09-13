namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    public interface IWorksOrderFactory
    {
        WorksOrder RaiseWorksOrder(WorksOrder worksOrderToBeRaised);
    }
}
