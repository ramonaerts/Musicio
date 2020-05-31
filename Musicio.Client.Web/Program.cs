﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Text;
using Blazored.LocalStorage;
using Howler.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.Extensions.DependencyInjection;
using Musicio.Client.Album;
using Musicio.Client.Artist;
using Musicio.Client.Converting;
using Musicio.Client.Playlist;
using Musicio.Client.Song;
using Musicio.Client.User;
using Musicio.Client.Validation;

namespace Musicio.Client.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("WEBASSEMBLY")))
            {
                WebAssemblyHttpMessageHandlerOptions.DefaultCredentials = FetchCredentialsOption.Include;
            }

            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<IValidationService, ValidationService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IPlaylistService, PlaylistService>();
            builder.Services.AddSingleton<IConvertingService, ConvertingService>();
            builder.Services.AddSingleton<ISessionService, SessionService>();
            builder.Services.AddSingleton<IArtistService, ArtistService>();
            builder.Services.AddSingleton<IAlbumService, AlbumService>();
            builder.Services.AddSingleton<ISongService, SongService>();
            builder.Services.AddSingleton<JwtSecurityTokenHandler, JwtSecurityTokenHandler>();
            builder.Services.AddScoped<IHowl, Howl>();
            builder.Services.AddScoped<IHowlGlobal, HowlGlobal>();
            builder.Services.AddAuthorizationCore();

            //await builder.Build().RunAsync();
            var host = builder.Build();

            //Check if user has an available token in their cookies
            var sessionService = host.Services.GetRequiredService<ISessionService>();
            if (await sessionService.CheckIfTokenIsAvailable())
            {
                HttpClientExtensions.WebToken = await sessionService.GetJwtCookie();
            }

            await host.RunAsync();
        }
    }
}
