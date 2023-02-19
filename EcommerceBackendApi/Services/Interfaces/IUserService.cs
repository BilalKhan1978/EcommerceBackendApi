using EcommerceBackendApi.Models;
using EcommerceBackendApi.ViewModels;

namespace EcommerceBackendApi.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUser(AddUserRequestDto addUserRequestDto);
        Task AddUsers(List<AddUserRequestDto> addUserRequestDto);
        Task LoginUser(LoginUserRequestDto loginUserRequestDto);
        Task<User> GetLoginUser();

        



    
    }
}
