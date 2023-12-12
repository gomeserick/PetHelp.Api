using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using PetHelp.Dtos;
using PetHelp.Dtos.Base;
using PetHelp.Filters;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;
using PetHelp.Services.Reflection;
using PetHelp.Services.verificator;
using System.ComponentModel.Design;

namespace PetHelp.Extensions
{
    public static class IServiceColletionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, ConfigurationManager config)
        {
            ConfigureSwagger(services);
            ConfigureOdata(services);
            ConfigureDbContext(services, config);
            ConfigureMapper(services);
            ConfigureDependencies(services);
        }
        private static void ConfigureDependencies(IServiceCollection builder)
        {
            builder.AddScoped<ODataVerificator<AdoptionDto>, ODataVerificator<AdoptionDto>>();
            builder.AddScoped<ODataVerificator<AnimalDto>, ODataVerificator<AnimalDto>>();
            builder.AddScoped<ODataVerificator<ClientDto>, ODataVerificator<ClientDto>>();
            builder.AddScoped<ODataVerificator<ClinicDto>, ODataVerificator<ClinicDto>>();
            builder.AddScoped<ODataVerificator<EmployeeDto>, ODataVerificator<EmployeeDto>>();
            builder.AddScoped<INotificatorService, NotificatorService>();
            builder.AddScoped<IODataReflectionService, ODataReflectionService>();
        }

        private static void ConfigureMapper(IServiceCollection builder)
        {
            builder.AddAutoMapper(typeof(IServiceColletionExtensions));
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Your API",
                    Version = "v1",
                    Description = "Your API Description",
                });
            });
        }

        private static void ConfigureOdata(IServiceCollection services)
        {
            ODataConventionModelBuilder builder = new();
            builder.EntitySet<AdoptionDto>("Adoption");
            builder.EntitySet<AnimalDto>("Animal");
            builder.EntitySet<ClientDto>("Client");
            builder.EntitySet<ClinicDto>("Clinic");
            builder.EntitySet<EmployeeDto>("Employee");

            services.AddControllers(e => e.Filters.Add(new ExceptionFilter()))
                .AddOData(options => {
                    options.AddRouteComponents("odata", builder.GetEdmModel())
                        .Select()
                        .Filter()
                        .OrderBy()
                        .SetMaxTop(20)
                        .Count()
                        .Expand()
                        .EnableQueryFeatures();
                });
        }

        private static void ConfigureDbContext(IServiceCollection services, ConfigurationManager config)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(config["ConnectionString"]));
            var debug = config["ConnectionString"];
        }
    }
}
