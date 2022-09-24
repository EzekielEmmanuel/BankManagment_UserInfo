using Application.BankManagement.Models;
using Application.BankManagement.Repositories;
using Application.Common.Services;
using Infrastructure.EF.Contexts;
using Infrastructure.EF.Models;
using Infrastructure.EF.Repositories;

namespace Infrastructure.BankManagement.Repositories;

internal sealed class
    BankAccountRepository : CrudMapRepository<BankAccount, BankAccountDto>, IBankAccountRepository
{
    private static readonly Func<BankAccount, BankAccountDto> MapFrom = x =>
        new BankAccountDto(x.Id, x.UserId, x.Number, x.Type);

    private static readonly Func<BankAccountDto, BankAccount> MapTo = x => new BankAccount
        {Id = x.Id, UserId = x.UserId, Number = x.Number, Type = x.Type};

    public BankAccountRepository(DataDbContext context, ICurrentUserService currentUserService) :
        base(
            new CrudRepository<BankAccount>(context, context.BankAccounts, currentUserService),
            MapFrom,
            MapTo
        )
    {
    }
}