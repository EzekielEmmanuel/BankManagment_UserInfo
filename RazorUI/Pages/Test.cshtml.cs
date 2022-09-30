using Application.BankAccounts.Models;
using Application.BankAccounts.Repositories;
using Application.Common.Services;
using Application.Users.Interfaces;
using Application.Users.Models;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorUI.Pages;

public class Test : PageModel
{
    private readonly IAuthService _authService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IBankAccountRepository _repository;

    public Test(IAuthService authService, ICurrentUserService currentUserService, IBankAccountRepository repository)
    {
        _authService = authService;
        _currentUserService = currentUserService;
        _repository = repository;
    }

    public void OnGet()
    {
    }

    public async Task OnPostDataTestButton()
    {
        // var id = _currentUserService.UserId;
        var item = new BankAccountDto(0, _currentUserService.UserId, "123456", BankAccountType.Checking);

        var result1 = await _repository.Insert(item);

        var result2 = await _repository.Get(x => x.Number == "123456");
        var items = new BankAccountDto[] { };
        result2.Match(value => { items = value.ToArray(); }, errors => { });

        result2 = await _repository.GetAll();
        items = new BankAccountDto[] { };
        result2.Match(value => { items = value.ToArray(); }, errors => { });

        var newItem = new BankAccountDto(0, _currentUserService.UserId, "555555", "Checking");
        var result3 = await _repository.GetById(items.Last().Id);
        result3.Match(
            value => { newItem = new BankAccountDto(value.Id, _currentUserService.UserId, "555555", "Checking"); },
            errors => { });

        var result4 = await _repository.Update(newItem);
        var result5 = await _repository.GetById(newItem.Id);
        result5.Match(value =>
        {
            var name = value.Number;
        }, errors => { });

        var result6 = await _repository.Delete(newItem.Id);
        var result7 = await _repository.GetById(newItem.Id);
    }

    public async Task OnPostCustomerLogin()
    {
        var result =
            await _authService.Login(new UserLoginDto {Email = "Customer@mail.com", Password = "Customer@123"});
    }

    public async Task OnPostTellerLogin()
    {
        var result = await _authService.Login(new UserLoginDto {Email = "Teller@mail.com", Password = "Teller@123"});
    }

    public async Task OnPostAdministratorLogin()
    {
        var result = await _authService.Login(new UserLoginDto
            {Email = "Administrator@mail.com", Password = "Administrator@123"});
    }

    public async Task OnPostLogout()
    {
        var result = await _authService.Logout();
    }
}