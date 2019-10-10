namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Resources;

    public interface ISerialNumberReissueService
    {
        IResult<SerialNumberReissue> ReissueSerialNumber(SerialNumberReissueResource resource);
    }
}
