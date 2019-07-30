namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using Linn.Common.Facade;
    using Linn.Production.Domain;
    using Linn.Production.Resources;

    public class ProductionMeasuresResourceBuilder : IResourceBuilder<ProductionMeasures>
    {
        public ProductionMeasuresResource Build(ProductionMeasures productionMeasures)
        {
            return new ProductionMeasuresResource
            {
                CitCode = productionMeasures.Cit.Code,
                CitName = productionMeasures.Cit.Name,
                CitBuildGroup = productionMeasures.Cit.BuildGroup,
                OverStockValue = productionMeasures.OverStockValue,
                NumberOfBackOrders = productionMeasures.NumberOfBackOrders,
                FFlaggedValue = productionMeasures.FFlaggedValue,
                ShortBat = productionMeasures.ShortBat,
                PtlJobref = productionMeasures.PtlJobref,
                DaysRequired = productionMeasures.DaysRequired,
                UsageValue = productionMeasures.UsageValue,
                AvgStockValue = productionMeasures.AvgStockValue,
                DeliveryPerformance1s = productionMeasures.DeliveryPerformance1s,
                ShortMetalwork = productionMeasures.ShortMetalwork,
                StockValue = productionMeasures.StockValue,
                BackOrderValue = productionMeasures.BackOrderValue,
                PboJobId = productionMeasures.PboJobId,
                OldestBackOrder = productionMeasures.OldestBackOrder?.ToString("o"),
                ShortProc = productionMeasures.ShortProc,
                DaysRequired3 = productionMeasures.DaysRequired3,
                DaysRequiredCanDo12 = productionMeasures.DaysRequiredCanDo12,
                UsageForTotalValue = productionMeasures.UsageForTotalValue,
                BuiltThisWeekQty = productionMeasures.BuiltThisWeekQty,
                BuiltThisWeekValue = productionMeasures.BuiltThisWeekValue,
                DaysRequiredCanDo3 = productionMeasures.DaysRequiredCanDo3,
                DeliveryPerformance2s = productionMeasures.DeliveryPerformance2s,
                FFlaggedQty = productionMeasures.FFlaggedQty,
                Fives = productionMeasures.Fives,
                Fours = productionMeasures.Fours,
                NumberOfPartsBackOrdered = productionMeasures.NumberOfPartsBackOrdered,
                Ones = productionMeasures.Ones,
                PboJobref = productionMeasures.PboJobref,
                ShortAny = productionMeasures.ShortAny,
                Threes = productionMeasures.Threes,
                Twos = productionMeasures.Twos
            };
        }

        public string GetLocation(ProductionMeasures productionMeasures)
        {
            return $"/production/measures/cit/{Uri.EscapeDataString(productionMeasures.Cit.Code)}";
        }

        object IResourceBuilder<ProductionMeasures>.Build(ProductionMeasures productionMeasures) => this.Build(productionMeasures);
    }
}