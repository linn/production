namespace Linn.Production.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class AteTestResourceBuilder : IResourceBuilder<AteTest>
    {
        public AteTestResource Build(AteTest test)
        {
            return new AteTestResource
                       {
                           TestId = test.TestId,
                           UserNumber = test.UserNumber,
                           DateTested = test.DateTested?.ToString("o"),
                           WorksOrderNumber = test.WorksOrderNumber,
                           NumberTested = test.NumberTested,
                           NumberOfSmtComponents = test.NumberOfSmtComponents,
                           NumberOfSmtFails = test.NumberOfSmtFails,
                           NumberOfPcbFails = test.NumberOfPcbFails,
                           NumberOfSmtBoardFails = test.NumberOfSmtBoardFails,
                           PcbOperator = test.PcbOperator,
                           MinutesSpent = test.MinutesSpent,
                           Machine = test.Machine,
                           PlaceFound = test.PlaceFound,
                           DateInvalid = test.DateInvalid?.ToString("o"),
                           FlowMachine = test.FlowMachine,
                           FlowSolderDate = test.FlowSolderDate?.ToString("o")
            };
        }

        object IResourceBuilder<AteTest>.Build(AteTest test) => this.Build(test);

        public string GetLocation(AteTest model)
        {
            throw new System.NotImplementedException();
        }
    }
}