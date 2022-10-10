using Application;
using Application.BankAccounts.Models;
using Application.BankAccounts.Repositories;
using Application.Common.Models;
using Application.Common.Services;
using Application.Users.Models;
using Infrastructure.BankAccounts.Models;
using Infrastructure.EF.Contexts;
using Infrastructure.EF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserInfoDto = Application.Users.Models.UserInfoDto;

namespace Infrastructure.Identity.Repositories;


internal sealed class
    
    UserInfoRepository : IUserInfoRepository
{
    private readonly UserDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private static readonly Func<ApplicationUser, UserInfoDto> MapFrom = x =>
        new UserInfoDto(x.Id, x.FirstName, x.LastName, x.Ssn, x.Dob, x.Address, x.PhoneNumber, x.Email);

    private static readonly Func<UserInfoDto, ApplicationUser> MapTo = x => new ApplicationUser { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Ssn = x.Ssn, Dob = x.Dob, Address = x.Address,PhoneNumber = x.Phone, Email = x.Email };


    public UserInfoRepository(UserDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<UserInfoDto>> GetById(string id)
    {
        var item = await _context.Users.FindAsync(id);
        if (item is null) return Result<UserInfoDto>.Failure("Failed to find item with given key");
        return Result<UserInfoDto>.Success(MapFrom(item));
    }

    public async Task<Result<IEnumerable<UserInfoDto>>> GetAll()
    {
        var items = await _context.Users.AsQueryable().ToArrayAsync();
        return Result<IEnumerable<UserInfoDto>>.Success(items.Select(MapFrom));
        
    }

    public async Task<Result<IEnumerable<UserInfoDto>>> Get(Func<UserInfoDto, bool> filter)
    {
        var items = await Task.FromResult(_context.Users.Select(MapFrom).Where(filter).ToArray());
        return Result<IEnumerable<UserInfoDto>>.Success(items);
    }
    

    public async Task<Result> Update(UserInfoDto item)
    {
        var oldItem = await _context.Users.FindAsync(item.Id);

        if (oldItem is not null)
        {
            _context.Entry(oldItem).CurrentValues.SetValues(item);
            _context.Users.Update(oldItem);
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
        var oldItem = await _context.Users.FindAsync(id);
        if (oldItem is not null)
        {
            _context.Users.Remove(oldItem);
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
}
