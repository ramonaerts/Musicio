using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Musicio.Core;
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

        public async Task<User> Login(string username, string password)
        {
            ApiResult result = await _httpClient.PostJsonAsync<ApiResult>("api/authentication", new
            {
                username,
                password
            });

            return result.GetData<User>();
        }
    }
}
