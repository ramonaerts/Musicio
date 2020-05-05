using Microsoft.EntityFrameworkCore;
using Musicio.Core.Domain;
using Musicio.Server.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Musicio.Server.Data
{
    public class MusicioContext : DbContext
    {
        public MusicioContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configurations = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
                (t.BaseType?.IsGenericType ?? false) && t.BaseType?.GetGenericTypeDefinition() == typeof(EntityMappingConfiguration<>));

            foreach (Type configurationType in configurations)
            {
                var configuration = (IEntityMappingConfiguration)Activator.CreateInstance(configurationType);
                configuration.ApplyConfiguration(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("User ID=lighttown;Password=pHmGfPMJ8LpV4CnPxZRy6wKTqAXdxi8nUKHw;Host=localhost;Port=5432;Database=Musicio;");
            optionsBuilder.UseMySql("Server=localhost;Database=musicio;Uid=root;");
        }
    }
}
