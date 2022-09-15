using Application.Common.Models;
using Application.Interfaces;
using Infrastructure.EF.Contexts;
using Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories;

public abstract class CrudRepository<TModel,TId>: ICrudRepository<TModel,TId> 
    where TModel : class
{
    private readonly DataDbContext _context;
    private readonly DbSet<TModel> _dbSet;
    protected CrudRepository(DataDbContext context, DbSet<TModel> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }
    public async Task<Result<TModel>> GetById(TId id)
    {
        var item = await _dbSet.FindAsync(id);
        if (item is null)
        {
            return Result<TModel>.Failure("Failed to find item with given key");
        }
        return Result<TModel>.Success(item);
    }

    public async Task<Result<IEnumerable<TModel>>> Get(Func<TModel,bool> filter)
    {

        var items = await _dbSet.Where(filter).AsQueryable().ToArrayAsync();
        return Result<TModel>.Success(items.AsEnumerable());
    }

    public async Task<Result> Insert(TModel data)
    {
        await _dbSet.AddAsync(data);
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