﻿using System.Threading.Tasks;
using DevnotMentor.Api.ActionFilters;
using DevnotMentor.Api.CustomEntities.Request.UserRequest;
using DevnotMentor.Api.Helpers.Extensions;
using DevnotMentor.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevnotMentor.Api.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("/users/login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var result = await userService.Login(request);

            return result.Success ? Success(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("/users/register")]
        public async Task<IActionResult> Register([FromForm] RegisterUserRequest request)
        {
            var result = await userService.Register(request);

            return result.Success ? Success(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("/users/change-password")]
        [ServiceFilter(typeof(TokenAuthentication))]
        public async Task<IActionResult> ChangePassword([FromBody] UpdatePasswordRequest request)
        {
            request.UserId = User.Claims.GetUserId();

            var result = await userService.ChangePassword(request);

            return result.Success ? Success(result) : BadRequest(result);
        }

        [HttpPatch]
        [Route("/users")]
        [ServiceFilter(typeof(TokenAuthentication))]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequest request)
        {
            request.UserId = User.Claims.GetUserId();

            var result = await userService.Update(request);

            return result.Success ? Success(result) : BadRequest(result);
        }

        [Route("/users/{email}/remind-password")]
        [HttpGet]
        public async Task<IActionResult> RemindPassword([FromRoute] string email)
        {
            var result = await userService.RemindPassword(email);

            return result.Success ? Success(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("/users/me/remind-password-complete")]
        public async Task<IActionResult> RemindPasswordCompleteAsync(CompleteRemindPasswordRequest request)
        {
            var result = await userService.RemindPasswordComplete(request);

            return result.Success ? Success(result) : BadRequest(result);
        }
    }
}