using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using Musicio.Core;
using Musicio.Core.Domain;
using Musicio.Core.Messages;
using Musicio.Server.Services.User;

namespace Musicio.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ApiResult> PostLogin(LoginMessage message)
        {
            User user = await _userService.LoginUser(message);

            var userModel = _mapper.Map<Core.Models.User>(user);

            userModel.Token = _userService.CreateToken(user.Id);

            return userModel != null ? ApiResult.Success(userModel) : ApiResult.BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<ApiResult> PostRegister(RegisterMessage message)
        {
            var success = await _userService.RegisterUser(message);

            return success ? ApiResult.Success(true) : ApiResult.BadRequest();
        }

        [HttpGet]
        [Route("{userId}")]
        public ApiResult GetUserById(int userId)
        {
            var user = _userService.GetUserById(userId);

            var userModel = _mapper.Map<Core.Models.User>(user);

            return ApiResult.Success(userModel);
        }

        [HttpPut]
        [Route("modify")]
        public ApiResult ChangeUserInfo(ChangeUserInfoMessage message)
        {
            var success = _userService.ChangeUserInfo(message);

            return success ? ApiResult.Success(true) : ApiResult.BadRequest();
        }
    }
}
