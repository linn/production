namespace Linn.Production.Facade.Services
{
    using Domain.LinnApps.SerialNumberReissue;

    using Linn.Common.Facade;

    using Resources;

    public interface ISerialNumberReissueService
    {
        IResult<SerialNumberReissue> ReissueSerialNumber(SerialNumberReissueResource resource);
    }
}
