using EcommerceBackendApi.Models;
using EcommerceBackendApi.ViewModels;

namespace EcommerceBackendApi.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUser(AddUserRequestDto addUserRequestDto);
        Task<User> GetUserByEmail(string email);
        Task<User> VerifyUser(string email, string password);

        //Task AddUsers(List<AddUserRequestDto> addUserRequestDto);
        //Task LoginUser(LoginUserRequestDto loginUserRequestDto);
        //Task<User> GetLoginUser();
    }
}
