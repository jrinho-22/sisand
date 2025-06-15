using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Infra;
using WebApplication1.Infra.Repositories;
using WebApplication1.Models.Services;

namespace WebApplication1.DI
{
    public static class Register
    {
        public static void DependencyInjection(IServiceCollection serviceCollection)
        {
            //Context(serviceCollection);
            Repositories(serviceCollection);
            Services(serviceCollection);
            //DomainEvents(serviceCollection);
        }

        private static void Services(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
        }

        private static void Repositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddTransient<ILoginRepository, LoginRepository>();
        }
    }
}
