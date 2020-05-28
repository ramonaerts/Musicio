using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Musicio.Client.User;
using Musicio.Core;
using Musicio.Core.Messages;
using Musicio.Core.Models;

namespace Musicio.Client.User
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionService _sessionService;
        private const string Path = "api/user";
        public UserService(HttpClient httpClient, ISessionService sessionService)
        {
            _httpClient = httpClient;
            _sessionService = sessionService;
        }

        public async Task<bool> Login(string mail, string password)
        {
            var result = await _httpClient.PostJsonAsync<ApiResult>(Path + "/login", new LoginMessage(mail, password));

            var user = result.GetData<Core.Models.User>();

            await _sessionService.SetCookie("WebToken", user.Token);

            return result.IsSuccess;
        }

        public async Task<bool> Register(string mail, string username, string password)
        {
            var result = await _httpClient.PostJsonAsync<ApiResult>(Path + "/register",
                new RegisterMessage(mail, username, password));
            return result.IsSuccess;
        }

        public async Task<Core.Models.User> GetUserInfo()
        {
            var result = await _httpClient.GetJsonAsync<ApiResult>(Path + "/@me");

            return result.GetData<Core.Models.User>();
        }

        public async Task<bool> ChangeUserInfo(string mail, string username, string newPassword, string oldPassword)
        {
            var result = await _httpClient.PutJsonAsync<ApiResult>(Path + "/@me",
                new ChangeUserInfoMessage(mail, username, newPassword, oldPassword));
            return result.IsSuccess;
        }
    }
}
