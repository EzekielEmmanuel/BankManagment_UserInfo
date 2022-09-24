using Application.BankManagment.Models;
using Application.BankManagment.Repositories;
using Application.Common.Models;
using Application.Common.Services;
using Infrastructure.EF.Contexts;
using Infrastructure.EF.Models;

namespace Infrastructure.EF.Repositories;

internal sealed class BankAccountRepository : CrudRepository<BankAccount>, IBankAccountRepository
{
    private readonly Func<BankAccount, BankAccountDto> _mapFrom = x =>
        new BankAccountDto(x.Id, x.UserId, x.Number, x.Type);

    private readonly Func<BankAccountDto, BankAccount> _mapTo = x => new BankAccount
        {Id = x.Id, UserId = x.UserId, Number = x.Number, Type = x.Type};

    public BankAccountRepository(DataDbContext context, ICurrentUserService currentUserService) : base(context,
        context.BankAccounts, currentUserService)
    {
    }

    public new async Task<Result<BankAccountDto>> GetById(int id)
    {
        var result = await base.GetById(id);

        return result.Match(value => { return Result<BankAccountDto>.Success(_mapFrom(value)); },
            errors => { return Result<BankAccountDto>.Failure(errors); });
    }

    public new async Task<Result<IEnumerable<BankAccountDto>>> GetAll()
    {
        var result = await base.GetAll();

        return result.Match(value =>
        {
            return Result<IEnumerable<BankAccountDto>>.Success(
                value.Select(x =>
                    _mapFrom(x)
                )
            );
        }, errors => { return Result<IEnumerable<BankAccountDto>>.Failure(errors); });
    }

    public async Task<Result<IEnumerable<BankAccountDto>>> Get(Func<BankAccountDto, bool> filter)
    {
        var result = await base.Get(_mapTo, _mapFrom, filter);

        return result;
    }

    public async Task<Result> Insert(BankAccountDto item)
    {
        var result = await base.Insert(_mapTo(item));
        return result;
    }

    public async Task<Result> Update(BankAccountDto item)
    {
        var result = await base.Update(_mapTo(item));
        return result;
    }

    public async Task<Result> Delete(BankAccountDto item)
    {
        var result = await base.Delete(item.Id);
        return result;
    }
}