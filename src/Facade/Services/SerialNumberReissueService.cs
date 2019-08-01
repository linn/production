namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    using Domain.LinnApps.Exceptions;
    using Domain.LinnApps.RemoteServices;
    using Domain.LinnApps.SerialNumberReissue;
    using Resources;

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
            if (!sernosRenumPack.ReissueSerialNumber(resource))
            {
                throw new InvalidSerialNumberReissueException(this.sernosRenumPack.GetSernosRenumMessage());
            }

            var sernos = this.serialNumberReissueRepository.FindBy(
                r => r.SerialNumber == resource.SerialNumber 
                     && r.ArticleNumber == resource.ArticleNumber 
                     && r.SernosGroup == resource.SernosGroup);

            var serialNumberReissue =  new SerialNumberReissue(sernos.SernosGroup, sernos.ArticleNumber)
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
