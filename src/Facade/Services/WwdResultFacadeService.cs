namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class WwdResultFacadeService : IWwdResultFacadeService
    {
        private readonly IWwdTrigFunction wwdTrigFunction;

        private readonly IRepository<ProductionTriggerLevel, string> productionTriggerLevelRepository;

        private readonly IQueryRepository<WwdDetail> wwdDetailRepository;

        public WwdResultFacadeService(IWwdTrigFunction wwdTrigFunction, IRepository<ProductionTriggerLevel, string> productionTriggerLevelRepository, IQueryRepository<WwdDetail> wwdDetailRepository)
        {
            this.wwdTrigFunction = wwdTrigFunction;
            this.productionTriggerLevelRepository = productionTriggerLevelRepository;
            this.wwdDetailRepository = wwdDetailRepository;
        }

        public IResult<WwdResult> GenerateWwdResultForTrigger(string partNumber, int? qty, string ptlJobref)
        {
            if (string.IsNullOrEmpty(partNumber))
            {
                return new BadRequestResult<WwdResult>("No part number supplied.");
            }

            if (qty == null)
            {
                return new BadRequestResult<WwdResult>("No qty supplied.");
            }

            var triggerLevel = productionTriggerLevelRepository.FindById(partNumber);

            if (triggerLevel == null)
            {
                return new NotFoundResult<WwdResult>("No production trigger level found");
            }

            if (string.IsNullOrEmpty(triggerLevel.WsName))
            {
                return new NotFoundResult<WwdResult>("No work station found");
            }

            var result = new WwdResult
            {
                PartNumber = partNumber,
                Qty = (int) qty,
                WorkStationCode = triggerLevel.WsName,
                PtlJobref = ptlJobref
            };


            result.WwdJobId = this.wwdTrigFunction.WwdTriggerRun(partNumber, (int) qty);

            if (result.WwdJobId == 0)
            {
                return new NotFoundResult<WwdResult>("Could not generate wwd work");
            }

            result.WwdDetails =
                this.wwdDetailRepository.FilterBy(d => d.WwdJobId == result.WwdJobId && d.PtlJobref == ptlJobref);

            return new SuccessResult<WwdResult>(result);
        }
    }
}
