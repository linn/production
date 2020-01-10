namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class AteTestResourceBuilder : IResourceBuilder<AteTest>
    {
        private readonly IResourceBuilder<AteTestDetail> detailResourceBuilder = new AteTestDetailResourceBuilder();

        public AteTestResource Build(AteTest test)
        {
            return new AteTestResource
                       {
                           TestId = test.TestId,
                           UserNumber = test.User.Id,
                           UserName = test.User.FullName,
                           DateTested = test.DateTested?.ToString("o"),
                           WorksOrderNumber = test.WorksOrder.OrderNumber,
                           NumberTested = test.NumberTested,
                           NumberOfSmtComponents = test.NumberOfSmtComponents,
                           NumberOfPcbComponents = test.NumberOfPcbComponents,
                           NumberOfSmtFails = test.NumberOfSmtFails,
                           NumberOfPcbFails = test.NumberOfPcbFails,
                           NumberOfSmtBoardFails = test.NumberOfSmtBoardFails,
                           PcbOperator = test.PcbOperator.Id,
                           PcbOperatorName = test.PcbOperator.FullName,
                           MinutesSpent = test.MinutesSpent,
                           Machine = test.Machine,
                           PlaceFound = test.PlaceFound,
                           DateInvalid = test.DateInvalid?.ToString("o"),
                           FlowMachine = test.FlowMachine,
                           PartNumber = test.WorksOrder.PartNumber,
                           PartDescription = test.WorksOrder.Part.Description,
                           WorksOrderQuantity = test.WorksOrder.Quantity,
                           FlowSolderDate = test.FlowSolderDate?.ToString("o"),
                           Details = test
                               .Details?.OrderBy(d => d.ItemNumber)
                               .Select(d => (AteTestDetailResource)this.detailResourceBuilder?.Build(d))
                       };
        }

        object IResourceBuilder<AteTest>.Build(AteTest test) => this.Build(test);

        public string GetLocation(AteTest model)
        {
            throw new System.NotImplementedException();
        }
    }
}