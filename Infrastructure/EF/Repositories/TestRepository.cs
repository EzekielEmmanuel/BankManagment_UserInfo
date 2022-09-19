using Application.Common.Models;
using Application.Common.Services;
using Infrastructure.EF.Contexts;
using Infrastructure.EF.Models;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories;

public class TestRepository : CrudRepository<TestModel, int>
{
    public TestRepository(DataDbContext context, ICurrentUserService currentUserService) : base(context, context.TestModels, currentUserService)
    {
    }
}