using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using PetHelp.Dtos;
using PetHelp.Dtos.Identity;
using PetHelp.Filters;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;
using System.Runtime.Intrinsics.X86;

namespace PetHelp.Extensions
{
    public static class IServiceColletionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, ConfigurationManager config)
        {
            ConfigureDbContext(services, config);
            ConfigureSwagger(services);
            ConfigureOdata(services);
            ConfigureMapper(services);
            ConfigureServices(services);
            ConfigureAuthentication(services);
        }

        private static void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthorization();

            services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);

            services.AddAuthorizationBuilder()
                .AddPolicy("api", p =>
                {
                    p.RequireAuthenticatedUser();
                    p.AddAuthenticationSchemes(IdentityConstants.BearerScheme);
                    p.
                });
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INotificatorService, NotificatorService>();
        }

        private static void ConfigureMapper(IServiceCollection builder)
        {
            builder.AddAutoMapper(typeof(IServiceColletionExtensions));
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Your API",
                    Version = "v1",
                    Description = "Your API Description",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    OpenIdConnectUrl = new Uri("https://localhost:7222/.well-known/openid-configuration"),
                    Name = "Authorization",
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri("https://localhost:7222/connect/authorize"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "api1", "Your API" }
                            }
                        }
                    }
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
            builder.EntitySet<MessageDto>("Message");
            builder.EntitySet<ScheduleDto>("Schedule");

            services.AddControllers(e =>
            {
                e.Filters.Add<ExceptionFilter>();
                e.Filters.Add<ValidationFilter>();
                e.Filters.Add<DatabaseFilter>();
            }).AddOData(options =>
            {
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
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });


            services.AddDefaultIdentity<IdentityBaseDto>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(0);
                options.Lockout.MaxFailedAccessAttempts = 500;

                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<DatabaseContext>()
            .AddApiEndpoints();
        }
    }
}
