using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public SessionService(IJSRuntime ijsRuntime, NavigationManager navigationManager)
        {
            _jsRuntime = ijsRuntime;
            _navigationManager = navigationManager;
        }

        public async Task SetCookie(string name, string value)
        {
            await _jsRuntime.InvokeVoidAsync("blazorExtensions.writeCookie", name, value, 7);

            HttpClientExtensions.WebToken = value;
        }

        public async Task<string> GetJwtCookie()
        {
            var cookieString = await _jsRuntime.InvokeAsync<string>("getCookies");

            if (string.IsNullOrEmpty(cookieString))
                return null;

            var cookieContainer = new CookieContainer();
            cookieContainer.SetCookies(new Uri(_navigationManager.BaseUri), cookieString);

            var cookieCollection = cookieContainer.GetCookies(new Uri(_navigationManager.BaseUri));

            var cookie = cookieCollection["WebToken"];

            return cookie.Value;
        }

        public async Task<bool> CheckIfTokenIsAvailable()
        {
            var cookieString = await _jsRuntime.InvokeAsync<string>("getCookies");

            if (string.IsNullOrEmpty(cookieString))
                return false;

            var cookieContainer = new CookieContainer();
            cookieContainer.SetCookies(new Uri(_navigationManager.BaseUri), cookieString);

            var cookieCollection = cookieContainer.GetCookies(new Uri(_navigationManager.BaseUri));

            var cookie = cookieCollection["WebToken"];

            if (cookie == null)
                return false;

            if (cookie.Expired)
                return false;

            return true;
        }

        public async Task RemoveCookies()
        {
            await _jsRuntime.InvokeVoidAsync("removeCookies");
        }
    }
}
