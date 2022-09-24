using Application.Common.Interfaces;
using Application.Common.Models;
using Infrastructure.EF.Models;

namespace Infrastructure.EF.Repositories;

internal abstract class CrudMapRepository<TEntity, TDto> : ICrudRepository<TDto, int>
    where TEntity : Entity
    where TDto : class
{
    private readonly Func<TEntity, TDto> _mapFrom;
    private readonly Func<TDto, TEntity> _mapTo;
    private readonly CrudRepository<TEntity> _repository;

    protected CrudMapRepository(CrudRepository<TEntity> repository, Func<TEntity, TDto> mapFrom,
        Func<TDto, TEntity> mapTo)
    {
        _repository = repository;
        _mapFrom = mapFrom;
        _mapTo = mapTo;
    }

    public async Task<Result<TDto>> GetById(int id)
    {
        var result = await _repository.GetById(id);

        return result.Match(value => { return Result<TDto>.Success(_mapFrom(value)); },
            errors => { return Result<TDto>.Failure(errors); });
    }

    public async Task<Result<IEnumerable<TDto>>> GetAll()
    {
        var result = await _repository.GetAll();

        return result.Match(value =>
        {
            return Result<IEnumerable<TDto>>.Success(
                value.Select(x =>
                    _mapFrom(x)
                )
            );
        }, errors => { return Result<IEnumerable<TDto>>.Failure(errors); });
    }

    public async Task<Result<IEnumerable<TDto>>> Get(Func<TDto, bool> filter)
    {
        var result = await _repository.Get(_mapTo, _mapFrom, filter);

        return result;
    }

    public async Task<Result> Insert(TDto item)
    {
        var result = await _repository.Insert(_mapTo(item));
        return result;
    }

    public async Task<Result> Update(TDto item)
    {
        var result = await _repository.Update(_mapTo(item));
        return result;
    }

    public async Task<Result> Delete(int id)
    {
        var result = await _repository.Delete(id);
        return result;
    }
}