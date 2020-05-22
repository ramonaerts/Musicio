using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Musicio.Core;
using Musicio.Core.Models;

namespace Musicio.Client.User
{
    public class SessionService : ISessionService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public Func<Task> OnAuthorizationChange { get; set; }

        public Core.Models.User _currentUser;

        public SessionService(HttpClient httpClient, IJSRuntime ijsRuntime, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jsRuntime = ijsRuntime;
            _navigationManager = navigationManager;
        }

        public async Task SetCookie(string name, string value)
        {
            await _jsRuntime.InvokeVoidAsync("blazorExtensions.writeCookie", name, value, 7);
        }

        public async Task<string> GetCookie()
        {
            var token = await _jsRuntime.InvokeAsync<string>("getCookies");

            Console.WriteLine(token);

            return string.IsNullOrEmpty(token) ? null : token;
        }

        public async Task DeleteCookie()
        {
            await _jsRuntime.InvokeVoidAsync("unsetCookies");
        }

        public async Task<bool> TryLoadAuthentication()
        {
            var cookieString = await _jsRuntime.InvokeAsync<string>("getCookies");

            if (string.IsNullOrEmpty(cookieString)) return false;
            return true;
        }

        public void SetCurrentUser(Core.Models.User user)
        {
            _currentUser = user;

            OnAuthorizationChange?.Invoke();
        }

        public Core.Models.User GetCurrentUser()
        {
            return _currentUser;
        }

        public bool IsAuthorized()
        {
            return _currentUser != null;
        }

        public async Task<bool> TryLoadLocalUser()
        {
            var cookieString = await _jsRuntime.InvokeAsync<string>("getCookies");

            if (string.IsNullOrEmpty(cookieString))
                return false;

            var cookieContainer = new CookieContainer();
            cookieContainer.SetCookies(new Uri(_navigationManager.BaseUri), cookieString);

            var cookieCollection = cookieContainer.GetCookies(new Uri(_navigationManager.BaseUri));

            var cookie = cookieCollection[".AspNetCore.Identity.Application"];

            if (cookie == null)
                return false;

            if (cookie.Expired)
                return false;

            return true;
        }

        public async Task LoadUser()
        {
            ApiResult result = await _httpClient.GetJsonAsync<ApiResult>("api/users/@me");

            SetCurrentUser(result.GetData<Core.Models.User>());
        }

        public async void UnloadUser()
        {
            SetCurrentUser(null);

            await _jsRuntime.InvokeVoidAsync("unsetCookies");

            OnAuthorizationChange?.Invoke();
        }
    }
}
