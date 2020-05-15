using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Musicio.Client.User;
using Musicio.Core;
using Musicio.Core.Messages;
using Musicio.Core.Models;

namespace Musicio.Client.User
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Login(string mail, string password)
        {
            var result = await _httpClient.PostJsonAsync<ApiResult>("api/User/login", new LoginMessage(mail, password));
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
