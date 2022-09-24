using Application.BankManagment.Models;
using Application.Common.Interfaces;

namespace Application.BankManagment.Repositories;

public interface IBankAccountRepository : ICrudRepository<BankAccountDto, int>
{
}