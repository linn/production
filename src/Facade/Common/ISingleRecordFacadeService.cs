namespace Linn.Production.Facade.Common
{
    using System.Collections.Generic;

    using Linn.Common.Facade;

    public interface ISingleRecordFacadeService<T, in TUpdateResource>
    {
        IResult<T> Get();

        IResult<ResponseModel<T>> Get(IEnumerable<string> privileges);

        IResult<T> Update(TUpdateResource updateResource);

        IResult<ResponseModel<T>> Update(TUpdateResource updateResource, IEnumerable<string> privileges);
    }
}