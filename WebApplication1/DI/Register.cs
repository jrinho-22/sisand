using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Infra;
using Azure.Communication.Email;
using WebApplication1.Infra.Repositories;
using WebApplication1.Models.Services;
using WebApplication1.Core;

namespace WebApplication1.DI
{
    public static class Register
    {
        public static void DependencyInjection(WebApplicationBuilder builder)
        {
            //Context(serviceCollection);
            Core(builder);
            Repositories(builder.Services);
            Services(builder.Services);
            //DomainEvents(serviceCollection);
        }
        private static void Core(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseAzureSql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var emailConnectionString = builder.Configuration.GetConnectionString("CommunicationServices");
            builder.Services.AddSingleton(new EmailClient(emailConnectionString));
        }

        private static void Services(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IEmailService, EmailService>();
            serviceCollection.AddTransient<IValidateEmailService, ValidateEmailService>();
        }

        private static void Repositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddTransient<ILoginRepository, LoginRepository>();
            serviceCollection.AddTransient<IValidateEmailRepository, ValidateEmailRepository>();
        }
    }
}
