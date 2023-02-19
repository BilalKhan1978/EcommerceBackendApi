using EcommerceBackendApi.Data;
using EcommerceBackendApi.Services.Implementations;
using EcommerceBackendApi.Services.Interfaces;
using EcommerceBackendApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequestDto addUserRequestDto)
        {
            try
            {
                await _userService.AddUser(addUserRequestDto);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [HttpPost("/api/users")]
        public async Task<IActionResult> AddUsers(List<AddUserRequestDto> addUserRequestDto)
        {
            try
            {
                await _userService.AddUsers(addUserRequestDto);
                return Ok("Users have been added successfully");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
