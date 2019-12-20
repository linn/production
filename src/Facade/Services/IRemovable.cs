namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;

    public interface IServiceWithRemove<T, in TKey, in TResource, in TUpdateResource> : IFacadeService<T, TKey, TResource, TUpdateResource>
    {
        IResult<T> Remove(T entity);
    }
}