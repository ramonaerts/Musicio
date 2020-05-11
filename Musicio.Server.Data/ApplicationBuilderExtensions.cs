using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Musicio.Server.Data
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Ensure that the database exists with all its tables and default data.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        public static void EnsureMigrated(this IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<MusicioContext>();

                var relationalDatabaseCreator =
                    (RelationalDatabaseCreator)dbContext.Database.GetService<IDatabaseCreator>();

                var databaseExists = relationalDatabaseCreator.Exists() && relationalDatabaseCreator.HasTables();

                dbContext.Database.Migrate();

                if (!databaseExists)
                {
                    //TODO: add standard data
                }
            }
        }
    }
}
