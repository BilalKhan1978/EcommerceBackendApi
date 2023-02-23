using EcommerceBackendApi.Services.Interfaces;
using EcommerceBackendApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> AddUser([FromBody] AddUserRequestDto addUserRequestDto)
        {
            if (!new EmailAddressAttribute().IsValid(addUserRequestDto.Email))
                return BadRequest("Email format is not correct");
                
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

        //[HttpPost("/api/users")]
        //public async Task<IActionResult> AddUsers([FromRoute] List<AddUserRequestDto> addUserRequestDto)
        //{
        //    try
        //    {
        //        await _userService.AddUsers(addUserRequestDto);
        //        return Ok("Users have been added successfully");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
    }
}
