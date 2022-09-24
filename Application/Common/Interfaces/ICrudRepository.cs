using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface ICrudRepository<TModel, in TId>
    where TModel : class
    where TId : notnull
{
    public Task<Result<TModel>> GetById(TId id);
    public Task<Result<IEnumerable<TModel>>> GetAll();
    public Task<Result<IEnumerable<TModel>>> Get(Func<TModel, bool> filter);
    public Task<Result> Insert(TModel item);
    public Task<Result> Update(TModel item);
    public Task<Result> Delete(TId id);
}