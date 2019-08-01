namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Domain.LinnApps.SerialNumberReissue;
    using Resources;

    public interface ISerialNumberReissueService
    {
        IResult<SerialNumberReissue> ReissueSerialNumber(SerialNumberReissueResource resource);
    }
}
