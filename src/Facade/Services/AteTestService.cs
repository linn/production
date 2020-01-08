namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class AteTestService : FacadeService<AteTest, int, AteTestResource, AteTestResource>
    {
        private readonly IRepository<WorksOrder, int> worksOrderRepository;

        public AteTestService(
            IRepository<AteTest, int> repository,
            ITransactionManager transactionManager,
            IRepository<WorksOrder, int> worksOrderRepository)
            : base(repository, transactionManager)
        {
            this.worksOrderRepository = worksOrderRepository;
        }

        protected override AteTest CreateFromResource(AteTestResource resource)
        {
            var worksOrder = this.worksOrderRepository.FindById(resource.WorksOrderNumber);

            return new AteTest
                       {
                           TestId = resource.TestId,
                           UserNumber = resource.UserNumber,
                           DateTested = resource.DateTested != null
                                            ? DateTime.Parse(resource.DateTested)
                                            : (DateTime?)null,
                           WorksOrder = worksOrder,
                           NumberTested = resource.NumberTested,
                           NumberOfSmtComponents = resource.NumberOfSmtComponents,
                           NumberOfSmtFails = resource.NumberOfSmtFails,
                           NumberOfPcbComponents = resource.NumberOfPcbComponents,
                           NumberOfPcbFails = resource.NumberOfPcbFails,
                           NumberOfPcbBoardFails = resource.NumberOfPcbBoardFails,
                           PcbOperator = resource.PcbOperator,
                           MinutesSpent = resource.MinutesSpent,
                           Machine = resource.Machine,
                           PlaceFound = resource.PlaceFound,
                           DateInvalid = resource.DateInvalid != null
                                             ? DateTime.Parse(resource.DateInvalid)
                                             : (DateTime?)null,
                           FlowMachine = resource.FlowMachine,
                           FlowSolderDate = resource.FlowSolderDate != null
                                                ? DateTime.Parse(resource.FlowSolderDate)
                                                : (DateTime?)null,
            };
        }

        protected override void UpdateFromResource(AteTest entity, AteTestResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<AteTest, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}