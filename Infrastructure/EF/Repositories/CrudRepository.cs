using Application.Common.Models;
using Application.Common.Services;
using Application.Interfaces;
using Infrastructure.EF.Contexts;
using Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories;

public abstract class CrudRepository<TModel, TId> : ICrudRepository<TModel, TId>
    where TModel : Entity
    where TId : notnull
{
    private readonly DataDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly DbSet<TModel> _dbSet;

    protected CrudRepository(DataDbContext context, DbSet<TModel> dbSet, ICurrentUserService currentUserService)
    {
        _context = context;
        _dbSet = dbSet;
        _currentUserService = currentUserService;
    }

    public async Task<Result<TModel>> GetById(TId id)
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
        var items = await _dbSet.Where(filter).AsQueryable().ToArrayAsync();
        return Result<IEnumerable<TModel>>.Success(items.AsEnumerable());
    }

    public async Task<Result> Insert(TModel item)
    {
        var userId = _currentUserService.UserId;
        item.MetaAddedUser = userId;
        item.MetaAddedDate = DateTimeOffset.UtcNow;

        await _dbSet.AddAsync(item);
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
        var userId = _currentUserService.UserId;
        item.MetaModifiedUser = userId;
        item.MetaModifiedDate = DateTimeOffset.UtcNow;

        _dbSet.Update(item);
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

    public async Task<Result> Delete(TModel item)
    {
        _dbSet.Remove(item);
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
}