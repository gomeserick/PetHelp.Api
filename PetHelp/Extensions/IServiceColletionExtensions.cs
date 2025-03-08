using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using PetHelp.Application.Contracts.Enums;
using PetHelp.Application.Security;
using PetHelp.Dtos;
using PetHelp.Dtos.Identity;
using PetHelp.Filters;
using PetHelp.Services.Context;
using PetHelp.Services.Context.Interfaces;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;
using PetHelp.Services.Seeders;
using PetHelp.Services.Seeders.Interfaces;
using System.Data;
using System.Security.Claims;

namespace PetHelp.Extensions
{
    public static class IServiceColletionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, ConfigurationManager config)
        {
            ConfigureDbContext(services);
            ConfigureSwagger(services);
            ConfigureOdata(services);
            ConfigureMapper(services);
            ConfigureServices(services);
            ConfigureAuthentication(services);
            ConfigureSeeders(services);
            ConfigureAppContext(services, config);
        }

        private static void ConfigureSeeders(IServiceCollection services)
        {
            services.AddKeyedScoped<ISeeder, UserSeeder>("UserSeeding");
            services.AddKeyedScoped<ISeeder, RoleSeeder>("RoleSeeding");
            services.AddKeyedScoped<ISeeder, ClaimSeeder>("ClaimSeeding");
        }

        private static void ConfigureAppContext(IServiceCollection services, ConfigurationManager config)
        {
            var singleton = new ApplicationContext()
            {
                Files = new() {
                    ImagePath = config.GetValue<string>("File:Images")
                }
            };

            services.AddSingleton(singleton);
        }

        private static void ConfigureAuthentication(IServiceCollection services)
        {

            services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme)
                .AddCookie(options =>
                {
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToAccessDenied = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return Task.CompletedTask;
                        },
                        OnRedirectToLogin = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();

            services.AddAuthorizationBuilder()
                .AddPolicy("Full", p =>
                {
                    p.RequireRole(PetHelpRoles.Employee);
                })
                .AddPolicy("Default", p =>
                {
                    p.RequireAuthenticatedUser();
                })
                .AddPolicy("EndUser", p =>
                {
                    p.RequireAuthenticatedUser();
                    p.RequireRole(PetHelpRoles.User);
                })
                .AddPolicy("Sysadm", p =>
                {
                    p.RequireAuthenticatedUser();
                    p.RequireRole(PetHelpRoles.SysAdm);
                })
                .AddPolicy("admin", p =>
                {
                    p.RequireRole(PetHelpRoles.SysAdm);
                })
                .AddPolicy("CreateAnimal", p =>
                {
                    p.RequireAuthenticatedUser();
                    p.RequireClaim(PetHelpClaims.CreateAnimal);
                })
                .AddPolicy("UpdateAnimal", p =>
                {
                    p.RequireAuthenticatedUser();
                    p.RequireClaim(PetHelpClaims.UpdateAnimal);
                })
                .AddPolicy("CreateVaccine", p =>
                {
                    p.RequireAuthenticatedUser();
                    p.RequireClaim(PetHelpClaims.CreateVaccine);
                })
                .AddPolicy("UpdateVaccine", p =>
                {
                    p.RequireAuthenticatedUser();
                    p.RequireClaim(PetHelpClaims.UpdateVaccine);
                })
                .AddPolicy("CreateMedication", p =>
                {
                    p.RequireAuthenticatedUser();
                    p.RequireClaim(PetHelpClaims.CreateMedication);
                })
                .AddPolicy("UpdateMedication", p =>
                {
                    p.RequireAuthenticatedUser();
                    p.RequireClaim(PetHelpClaims.UpdateMedication);
                })
                .AddPolicy("CreateClinic", p =>
                {
                    p.RequireAuthenticatedUser();
                    p.RequireClaim(PetHelpClaims.CreateClinic);
                })
                .AddPolicy("UpdateClinic", p =>
                {
                    p.RequireClaim(PetHelpClaims.UpdateClinic);
                })
                .AddPolicy("ConfirmAdoption", p =>
                {
                    p.RequireClaim(PetHelpClaims.ConfirmAdoption);
                })
                .AddPolicy("CreateAdoption", p =>
                {
                    p.RequireClaim(PetHelpClaims.CreateAdoption);
                })
                .AddPolicy("AddToWatchList", p =>
                {
                    p.RequireClaim(PetHelpClaims.AddToWatchList);
                })
                .AddPolicy("RemoveFromWatchList", p =>
                {
                    p.RequireClaim(PetHelpClaims.RemoveFromWatchList);
                });
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IContext, PetHelpContext>();
            services.AddScoped<INotificatorService, NotificatorService>();
            services.AddSingleton<NotificatorService>();
        }

        private static void ConfigureMapper(IServiceCollection builder)
        {
            builder.AddAutoMapper(typeof(IServiceColletionExtensions).Assembly);
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddHttpLogging(o =>
            {
                o.LoggingFields = HttpLoggingFields.All;
                o.RequestHeaders.Add("sec-ch-ua");
                o.ResponseHeaders.Add("MyResponseHeader");
                o.MediaTypeOptions.AddText("application/javascript");
                o.RequestBodyLogLimit = 4096;
                o.ResponseBodyLogLimit = 4096;
                o.CombineLogs = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Your API",
                    Version = "v1",
                    Description = "Your API Description",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        private static void ConfigureOdata(IServiceCollection services)
        {
            ODataConventionModelBuilder builder = new();
            builder.EntitySet<AnimalDto>("Animal").IgnorePrivateData();
            builder.EntitySet<ClinicDto>("Clinic").IgnorePrivateData();
            builder.EntitySet<IdentityBaseDto>("SysAdmEmployee")
                .Ignoring(e => e.PasswordHash)
                .Ignoring(e => e.PasswordHash)
                .Ignoring(e => e.ConcurrencyStamp)
                .Ignoring(e => e.AccessFailedCount)
                .Ignoring(e => e.LockoutEnd)
                .Ignoring(e => e.EmailConfirmed)
                .Ignoring(e => e.SecurityStamp)
                .Ignoring(e => e.LockoutEnabled)
                .Ignoring(e => e.TwoFactorEnabled)
                .Ignoring(e => e.PhoneNumberConfirmed)
                .Ignoring(e => e.NormalizedUserName)
                .Ignoring(e => e.NormalizedEmail)
                .Ignoring(e => e.User);

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

        private static void ConfigureDbContext(IServiceCollection services)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            services.AddDbContext<DatabaseContext>(options =>
            {
                var host = Environment.GetEnvironmentVariable("DB_HOST");
                var name = Environment.GetEnvironmentVariable("DB_NAME");
                var pass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

                var connectionString = $"Data Source={host};Initial Catalog={name};User ID=sa;Password={pass};Persist Security Info=False;Encrypt=False";

                var a = options.UseSqlServer(connectionString, e =>
                {
                    //e.EnableRetryOnFailure();
                });
            });


            services.AddDefaultIdentity<IdentityBaseDto>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false; // true;
                options.Password.RequiredUniqueChars = 1;


                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(0);
                options.Lockout.MaxFailedAccessAttempts = 500;

                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<DatabaseContext>()
            .AddApiEndpoints();
        }
    }
}
