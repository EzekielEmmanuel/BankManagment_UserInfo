using Application.BankAccounts.Models;
using Application.Common.Interfaces;

namespace Application.BankAccounts.Repositories;

public interface IBankAccountRepository : ICrudRepository<BankAccountDto, int>
{
}