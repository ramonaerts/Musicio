using Microsoft.Extensions.DependencyInjection;
using Musicio.Core.Data;
using Musicio.Core.Domain;
using System;
using System.Text;

namespace Musicio.Server.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<IRepository<User>, Repository<User>>();
            serviceCollection.AddScoped<IRepository<Playlist>, Repository<Playlist>>();

            return serviceCollection;
        }
    }
}
