using Microsoft.EntityFrameworkCore;
using CourseProject.Infrastructure;
using CourseProject.Infrastructure.Repositories;
using CourseProject.Domain.Abstractions;

namespace CourseProject.Web.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DbConnection"), b =>
                b.MigrationsAssembly("CourseProject.Infrastructure")));
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
		services.AddScoped<IEmployeeRepository, EmployeeRepository>();
		services.AddScoped<ISubscriberRepository, SubscriberRepository>();
		services.AddScoped<ITariffPlanRepository, TariffPlanRepository>();
		services.AddScoped<IServiceContractRepository, ServiceContractRepository>();
		services.AddScoped<IServiceStatisticRepository, ServiceStatisticRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
