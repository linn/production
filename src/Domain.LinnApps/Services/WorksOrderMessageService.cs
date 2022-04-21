namespace Linn.Production.Domain.LinnApps.Services
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderMessageService : IWorksOrderMessageService
    {
        private readonly IRepository<WorksOrderMessage, string> worksOrderMessageRepository;

        public WorksOrderMessageService(IRepository<WorksOrderMessage, string> worksOrderMessageRepository)
        {
            this.worksOrderMessageRepository = worksOrderMessageRepository;
        }

        public string GetMessage(string partNumber)
        {
            var messageObject = this.worksOrderMessageRepository.FindById(partNumber);

            return messageObject != null ? messageObject.Message : string.Empty;
        }
    }
}
