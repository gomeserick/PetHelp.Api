using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Database;
using PetHelp.Services.Seeders.Interfaces;

namespace PetHelp.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void ConfigureApp(this WebApplication app)
        {
            ConfigureCors(app);
            ConfigureSwaggerUI(app);
            ConfigureDatabase(app);
            UseAuthentication(app);
        }

        private static void ConfigureCors(WebApplication app)
        {
            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });
        }

        private static void UseAuthentication(WebApplication app)
        {
            app.MapIdentityApi<IdentityBaseDto>();
            app.UseAuthentication();
            app.UseAuthorization();
        }

        private static void ConfigureSwaggerUI(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
                c.RoutePrefix = "swagger";
            });
        }

        private static async void ConfigureDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                await scope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.MigrateAsync();
                await scope.ServiceProvider.GetKeyedService<ISeeder>("RoleSeeding").Seed();
                await scope.ServiceProvider.GetKeyedService<ISeeder>("UserSeeding").Seed();
            }
        }
    }
}
