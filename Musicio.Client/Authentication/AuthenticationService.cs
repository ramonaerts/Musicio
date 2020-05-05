using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Musicio.Core;
using Musicio.Core.Messages;
using Musicio.Core.Models;

namespace Musicio.Client.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Login(string username, string password)
        {
            var result = await _httpClient.PostJsonAsync<ApiResult>("api/User/login", new LoginMessage(username, password));
            return result.IsSuccess;
        }

        public async Task<bool> Register(string mail, string username, string password)
        {
            var result = await _httpClient.PostJsonAsync<ApiResult>("api/User/register",
                new RegisterMessage(mail, username, password));
            return result.IsSuccess;
        }
    }
}
