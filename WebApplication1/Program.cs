using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using WebApplication1.DI;
using WebApplication1.Infra;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        Register.DependencyInjection(builder);

        //builder.Services.AddDbContext<AppDbContext>(options =>
            //options.UseAzureSql(builder.Configuration.GetConnectionString("DefaultConnection")));


        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();


    }
}