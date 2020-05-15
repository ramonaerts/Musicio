using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using Musicio.Core;
using Musicio.Core.Domain;
using Musicio.Core.Messages;
using Musicio.Server.Services.User;

namespace Musicio.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _authenticationService;
        private readonly IMapper _mapper;

        public UserController(IUserService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ApiResult> PostLogin(LoginMessage message)
        {
            User user = await _authenticationService.LoginUser(message);

            var userModel = _mapper.Map<Core.Models.User>(user);

            return userModel != null ? ApiResult.Success(userModel) : ApiResult.BadRequest();
        }

        [HttpPost]
        [Route("register")]
        public async Task<ApiResult> PostRegister(RegisterMessage message)
        {
            var success = await _authenticationService.RegisterUser(message);

            return success ? ApiResult.Success(success) : ApiResult.BadRequest();
        }
    }
}
