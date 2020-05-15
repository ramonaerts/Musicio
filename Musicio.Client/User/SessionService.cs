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
    public class SessionService
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
