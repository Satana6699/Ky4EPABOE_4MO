using CourseProject.Web;

public static class MiddlewareExtensions
{
    public static async Task UseDatabaseSeeder(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<DbInitialazer>();
        await seeder.InitializeAsync();
    }
}
