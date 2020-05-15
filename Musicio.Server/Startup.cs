using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Musicio.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DbContext, MusicioContext>();
            services.AddDbContext<MusicioContext>();
            services.AddControllers();
            services.AddScoped<IRepository<BaseEntity>, Repository<BaseEntity>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Playlist>, Repository<Playlist>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlaylistService, PlaylistService>();
            services.AddScoped<IFileManagementService, FileManagementService>();
            /*services.AddRepositories();
            services.AddServices();*/

            services.AddAutoMapper(typeof(AutoMapperProfile));

            
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
