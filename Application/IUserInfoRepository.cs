using Application.Common.Models;
using Application.Users.Models;

namespace Application;

    public interface IUserInfoRepository
    {
        Task<Result<UserInfoDto>> GetById(string id);
        Task<Result<IEnumerable<UserInfoDto>>> GetAll();
        Task<Result<IEnumerable<UserInfoDto>>> Get(Func<UserInfoDto, bool> filter);
        Task<Result> Update(UserInfoDto item);
        Task<Result> Delete(int id);
    } 
