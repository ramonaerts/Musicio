using Microsoft.Extensions.DependencyInjection;
using Musicio.Core.Data;
using Musicio.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Server.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRepository<User>, Repository<User>>();

            return serviceCollection;
        }
    }
}
