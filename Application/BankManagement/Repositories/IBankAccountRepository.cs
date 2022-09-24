using Application.BankManagement.Models;
using Application.Common.Interfaces;

namespace Application.BankManagement.Repositories;

public interface IBankAccountRepository : ICrudRepository<BankAccountDto, int>
{
}