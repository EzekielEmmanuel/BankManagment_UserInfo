using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Services;
using Infrastructure.EF.Contexts;
using Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories;

internal sealed class CrudRepository<TModel> : ICrudRepository<TModel, int>
    where TModel : Entity
{
    private readonly DataDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly DbSet<TModel> _dbSet;

    public CrudRepository(DataDbContext context, DbSet<TModel> dbSet, ICurrentUserService currentUserService)
    {
        _context = context;
        _dbSet = dbSet;
        _currentUserService = currentUserService;
    }

    public async Task<Result<TModel>> GetById(int id)
    {
        var item = await _dbSet.FindAsync(id);
        if (item is null) return Result<TModel>.Failure("Failed to find item with given key");
        return Result<TModel>.Success(item);
    }

    public async Task<Result<IEnumerable<TModel>>> GetAll()
    {
        var items = await _dbSet.AsQueryable().ToArrayAsync();
        return Result<IEnumerable<TModel>>.Success(items.AsEnumerable());
    }

    public async Task<Result<IEnumerable<TModel>>> Get(Func<TModel, bool> filter)
    {
        var items = await Task.FromResult(_dbSet.Where(filter).ToArray());
        return Result<IEnumerable<TModel>>.Success(items);
    }

    public async Task<Result> Insert(TModel item)
    {
        var userId = _currentUserService.UserId;
        item.MetaAddedUser = userId;
        item.MetaAddedDate = DateTimeOffset.UtcNow;

        _dbSet.Add(item);
        try
        {
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure("Failed to add item");
        }
    }

    public async Task<Result> Update(TModel item)
    {
        var oldItem = await _dbSet.FindAsync(item.Id);

        if (oldItem is not null)
        {
            var metaAddedUser = oldItem.MetaAddedUser;
            var metaAddedDate = oldItem.MetaAddedDate;

            _context.Entry(oldItem).CurrentValues.SetValues(item);

            var userId = _currentUserService.UserId;
            oldItem.MetaModifiedUser = userId;
            oldItem.MetaModifiedDate = DateTimeOffset.UtcNow;

            oldItem.MetaAddedUser = metaAddedUser;
            oldItem.MetaAddedDate = metaAddedDate;

            _dbSet.Update(oldItem);
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure("Failed to update item");
            }
        }

        return Result.Failure("Failed to update item");
    }

    public async Task<Result> Delete(int id)
    {
        var oldItem = await _dbSet.FindAsync(id);
        if (oldItem is not null)
        {
            _dbSet.Remove(oldItem);
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure("Failed to delete item");
            }
        }

        return Result.Failure("Failed to delete item");
    }

    public async Task<Result<IEnumerable<TOut>>> Get<TOut>(Func<TOut, TModel> mapTo, Func<TModel, TOut> mapFrom,
        Func<TOut, bool> filter)
        where TOut : class
    {
        var items = await Task.FromResult(_dbSet.Select(mapFrom).Where(filter).ToArray());
        return Result<IEnumerable<TOut>>.Success(items);
    }
}