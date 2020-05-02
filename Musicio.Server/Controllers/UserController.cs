using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using Musicio.Core;
using Musicio.Core.Domain;
using Musicio.Core.Messages;
using Musicio.Core.Models;
using Musicio.Server.Services.Authentication;

namespace Musicio.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //private readonly service _authenticationService;
        private readonly IAuthenticationService _authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ApiResult> PostLogin(LoginMessage message)
        {
            var testUser = new Core.Models.User(1, "Ramon", "Aerts", 1);
            return ApiResult.Success(testUser);
        }
    }
}
