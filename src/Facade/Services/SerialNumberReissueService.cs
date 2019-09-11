namespace Linn.Production.Facade.Services
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Facade.Extensions;
    using Linn.Production.Resources;

    public class SerialNumberReissueService : ISerialNumberReissueService
    {
        private readonly ISernosRenumPack sernosRenumPack;

        private readonly IRepository<SerialNumberReissue, int> serialNumberReissueRepository;

        public SerialNumberReissueService(ISernosRenumPack sernosRenumPack, IRepository<SerialNumberReissue, int> serialNumberReissueRepository)
        {
            this.sernosRenumPack = sernosRenumPack;
            this.serialNumberReissueRepository = serialNumberReissueRepository;
        }

        public IResult<SerialNumberReissue> ReissueSerialNumber(SerialNumberReissueResource resource)
        {
            var employee = resource.Links.FirstOrDefault(a => a.Rel == "created-by");

            if (employee == null)
            {
                return new BadRequestResult<SerialNumberReissue>("Must supply an employee number when Reissuing Serial Numbers");
            }

            resource.CreatedBy = employee.Href.ParseId();

            var sernosRenumMessage = this.sernosRenumPack.ReissueSerialNumber(resource);

            if (sernosRenumMessage != "SUCCESS")
            {
                return new BadRequestResult<SerialNumberReissue>(sernosRenumMessage);
            }

            var sernos = this.serialNumberReissueRepository.FindBy(
                r => r.SerialNumber == resource.SerialNumber
                     && r.ArticleNumber == resource.ArticleNumber
                     && r.SernosGroup == resource.SernosGroup);

            var serialNumberReissue = new SerialNumberReissue(sernos.SernosGroup, sernos.ArticleNumber)
                                          {
                                              SerialNumber = sernos.SerialNumber,
                                              NewSerialNumber = sernos.NewSerialNumber,
                                              NewArticleNumber = sernos.NewArticleNumber,
                                              CreatedBy = sernos.CreatedBy,
                                              Id = sernos.Id,
                                              Comments = sernos.Comments
                                          };

            return new CreatedResult<SerialNumberReissue>(serialNumberReissue);
        }
    }
}
