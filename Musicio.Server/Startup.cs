using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Howler.Blazor.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Musicio.Core;
using Musicio.Core.Data;
using Musicio.Core.Domain;
using Musicio.Server.Data;
using Musicio.Server.Services.FileManagement;
using Musicio.Server.Services.Playlist;
using Musicio.Server.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Musicio.Server.Services.Album;
using Musicio.Server.Services.Artist;
using Newtonsoft.Json;

namespace Musicio.Server
{
    public class Startup
    {
        readonly string AllowOrigins = "_AllowOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowOrigins,
                    builder =>
                    {
                        builder.WithOrigins("https://musicio.azurewebsites.net",
                                            "http://musicio.azurewebsites.net",
                                            "https://localhost:5001")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            
            services.AddSingleton<DbContext, MusicioContext>();
            services.AddDbContext<MusicioContext>();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            services.AddScoped<IRepository<BaseEntity>, Repository<BaseEntity>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Playlist>, Repository<Playlist>>();
            services.AddScoped<IRepository<Artist>, Repository<Artist>>();
            services.AddScoped<IRepository<Album>, Repository<Album>>();
            services.AddScoped<IRepository<PlaylistSong>, Repository<PlaylistSong>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlaylistService, PlaylistService>();
            services.AddScoped<IFileManagementService, FileManagementService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IAlbumService, AlbumService>();
            /*services.AddRepositories();
            services.AddServices();*/

            services.AddAutoMapper(typeof(AutoMapperProfile));

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = false;
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseClientSideBlazorFiles<Client.Web.Program>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToClientSideBlazor<Client.Web.Program>("index.html");
            });

            app.EnsureMigrated();
        }
    }
}
