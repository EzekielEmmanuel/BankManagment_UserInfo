using Application.Common.Services;
using Application.UserManagment.Interfaces;
using Application.UserManagment.Models;
using Infrastructure.EF.Models;
using Infrastructure.EF.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorUI.Pages;

public class Test : PageModel
{
    private readonly IAuthService _authService;
    private readonly ICurrentUserService _currentUserService;
    private readonly TestRepository _repository;

    public Test(IAuthService authService, ICurrentUserService currentUserService, TestRepository repository)
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
        var item = new TestModel
        {
            name = "name1",
            value = 100
        };

        var result1 = await _repository.Insert(item);

        var result2 = await _repository.GetAll();
        var items = new TestModel[] { };
        result2.Match(value => { items = value.ToArray(); }, errors => { });

        var newItem = new TestModel
        {
            name = "name1",
            value = 100
        };
        var result3 = await _repository.GetById(items.Last().Id);
        result3.Match(value =>
        {
            newItem = value;
            newItem.name = "updated name";
        }, errors => { });

        var result4 = await _repository.Update(newItem);
        var result5 = await _repository.GetById(newItem.Id);
        result5.Match(value =>
        {
            var name = value.name;
        }, errors => { });

        var result6 = await _repository.Delete(newItem);
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