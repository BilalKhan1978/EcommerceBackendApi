﻿using EcommerceBackendApi.Services.Interfaces;
using EcommerceBackendApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace EcommerceBackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;   
        }
        [HttpPost]
        [Authorize(Roles = "super-admin")]
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
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }
        }

        [HttpGet("allusers")]
        [Authorize(Roles = "super-admin")]
        public async Task<IActionResult> GetAllUsersData()
        {
            try
            { 
            return Ok(await _userService.GetAllUsersData());
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "super-admin")]
        public async Task<IActionResult> DeleteUserById([FromRoute] int id)
        {
            try
            {
                _userService.DeleteUserById(id);
                return Ok("Desired user has been successfully deleted / removed");
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                if (e.Message.Contains("No user found"))
                    NotFound("There is no user to delete");
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
