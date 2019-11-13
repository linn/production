namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public interface IPartFailSupplierService
    {
        SuccessResult<IEnumerable<PartFailSupplierView>> GetAll();
    }
}