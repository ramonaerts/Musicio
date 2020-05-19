using Musicio.Core.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Protected;
using Musicio.Server.Data;

namespace Musicio.Server.Tests
{
    public static class MockExtensions
    {
        public static void SetupRepositoryMock<T>(this Mock<Repository<T>> repositoryMock, Action<IRepository<T>> options) where T : BaseEntity
        {
            var context = new MusicioTestContext();
            context.Database.EnsureDeleted();

            var constructorArguments = repositoryMock.GetType().GetField("constructorArguments",
                BindingFlags.NonPublic | BindingFlags.Instance);
            constructorArguments?.SetValue(repositoryMock, new object[] { context });

            repositoryMock.Protected().SetupGet<DbSet<T>>("Entities").Returns(context.Set<T>());
            repositoryMock.Setup(repository => repository.Table).Returns(context.Set<T>());
            repositoryMock.Setup(repository => repository.TableNoTracking).Returns(context.Set<T>().AsNoTracking);

            options.Invoke(repositoryMock.Object);

            context.SaveChanges();
        }
    }
}
