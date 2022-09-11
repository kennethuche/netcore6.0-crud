using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Reflection;
using Toyin_group_api.Core.Data;
using Toyin_group_api.Core.Services;
using Xunit;

namespace Toyin_group_test
{
    public class BaseTest
    {

        protected IServiceCollection GetCollection()
        {
            IServiceCollection service = new ServiceCollection();

            var dbName = Guid.NewGuid().ToString();
            service.AddDbContext<AppDbContext>(ctx =>
            {
                ctx.UseInMemoryDatabase(dbName);
            }, ServiceLifetime.Scoped);


            service.AddScoped(xy =>
            {
                var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(dbName)
                .Options;

                var mock = new Mock<IAppDbContextFactory>();
                mock.Setup(x => x.ResolveDbContext()).Returns(new AppDbContext(options));
                return mock.Object;
            });

            ServicesBootstrapper.InitServices(service);

            service.AddLogging();
        

            service.AddMediatR(Assembly.GetExecutingAssembly());
       


            return service;
        }
    }
}
