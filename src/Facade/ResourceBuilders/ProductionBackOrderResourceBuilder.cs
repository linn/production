namespace Linn.Production.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Resources;

    public class ProductionBackOrderResourceBuilder : IResourceBuilder<ProductionBackOrder>
    {
        public ProductionBackOrderResource Build(ProductionBackOrder order)
        {
            return new ProductionBackOrderResource
            {
                JobId = order.JobId,
                OrderNumber = order.OrderNumber,
                OrderLine = order.OrderLine,
                ArticleNumber = order.ArticleNumber,
                CitCode = order.CitCode,
                DatePossible = order.DatePossible?.ToString("o"),
                RequestedDeliveryDate = order.RequestedDeliveryDate?.ToString("o"),
                QueuePosition = order.QueuePosition,
                BaseValue = order.BaseValue,
                BackOrderQty = order.BackOrderQty
            };
        }

        object IResourceBuilder<ProductionBackOrder>.Build(ProductionBackOrder order) => this.Build(order);

        public string GetLocation(ProductionBackOrder model)
        {
            throw new System.NotImplementedException();
        }
    }
}