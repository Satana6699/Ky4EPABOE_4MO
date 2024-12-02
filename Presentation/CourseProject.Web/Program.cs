using AutoMapper;
using CourseProject.Application;
using CourseProject.Web.Extensions;
using CourseProject.Application.Requests.Queries;
using CourseProject.Application.RequestHandlers.QueryHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CourseProject.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.ConfigureCors();

            builder.Services.ConfigureDbContext(builder.Configuration);

            builder.Services.ConfigureServices();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper autoMapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(autoMapper);

            // Конфигурация JWT
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(GetEmployeesQuery).Assembly));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddRazorPages().AddRazorOptions(options =>
            {
                options.PageViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
            });
            builder.Services.AddScoped<DbInitialazer>();
            var app = builder.Build();
            app.UseStaticFiles();
            await app.UseDatabaseSeeder();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.MapControllers();
                
            app.MapRazorPages();

            app.MapGet("/", () => Results.Redirect("/Home/Home"));
            app.MapFallbackToPage("/Home/Home");

            app.UseAuthentication(); // Если используется
            app.UseAuthorization();  // Вызов авторизации

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}