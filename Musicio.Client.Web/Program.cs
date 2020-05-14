﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Text;
using Blazored.LocalStorage;
using Howler.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.Extensions.DependencyInjection;
using Musicio.Client.Authentication;
using Musicio.Client.Converting;
using Musicio.Client.Playlist;
using Musicio.Client.Validation;

namespace Musicio.Client.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            /*if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("WEBASSEMBLY")))
            {
                WebAssemblyHttpMessageHandlerOptions.DefaultCredentials = FetchCredentialsOption.Include;
            }*/

            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<IValidationService, ValidationService>();
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            builder.Services.AddSingleton<IPlaylistService, PlaylistService>();
            builder.Services.AddSingleton<IConvertingService, ConvertingService>();
            builder.Services.AddScoped<IHowl, Howl>();
            builder.Services.AddScoped<IHowlGlobal, HowlGlobal>();

            await builder.Build().RunAsync();
        }
    }
}
