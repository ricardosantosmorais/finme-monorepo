using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.SimpleEmail;
using Amazon.SimpleNotificationService;
using Finme.Api.Admin.Middleware;
using Finme.Application.Admin.Behaviors;
using Finme.Application.Admin.Features.Auth.Commands;
using Finme.Domain.Interfaces;
using Finme.Infrastructure.Data;
using Finme.Infrastructure.Interfaces;
using Finme.Infrastructure.Repositories;
using Finme.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Finme.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole().SetMinimumLevel(LogLevel.Debug);
                });
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogInformation("Starting application...");

                // Log assemblies carregados
                logger.LogDebug("Loaded assemblies: {Assemblies}",
                    string.Join(", ", AppDomain.CurrentDomain.GetAssemblies().Select(a => a.GetName().Name)));

                // Cria o builder da aplicação web
                var builder = WebApplication.CreateBuilder(args);

                // Configurações principais
                ConfigureDatabase(builder, logger);
                ConfigureMediatR(builder, logger);
                ConfigureAuthentication(builder);
                ConfigureLogging(builder);
                ConfigureValidators(builder);
                ConfigureAutoMapper(builder);
                ConfigureAWS(builder);
                ConfigureServices(builder, logger);
                ConfigureControllers(builder);

                // Constrói a aplicação
                logger.LogInformation("Building application...");
                var app = builder.Build();

                // Aplica migrações do banco
                ApplyDatabaseMigrations(app, logger);

                // Configura o pipeline
                ConfigureMiddleware(app);

                // Inicia a aplicação
                logger.LogInformation("Running application...");
                app.Run();
            }
            catch (Exception ex)
            {
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole().SetMinimumLevel(LogLevel.Debug);
                });
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogCritical(ex, "Application failed to start: {Message}", ex.Message);
                throw;
            }
        }

        private static void ConfigureDatabase(WebApplicationBuilder builder, ILogger<Program> logger)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            logger.LogInformation("Configuring database with connection string: {ConnectionString}", connectionString);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging(); // Ativa logs detalhados para SQL (apenas em dev)
            });
        }

        private static void ConfigureMediatR(WebApplicationBuilder builder, ILogger<Program> logger)
        {
            logger.LogInformation("Configuring MediatR...");
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
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
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                        logger.LogWarning($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                        logger.LogInformation("Token validated successfully");
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                        logger.LogDebug("Attempting to parse JWT from Authorization header");
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private static void ConfigureLogging(WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
        }

        private static void ConfigureValidators(WebApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssembly(
                typeof(Finme.Application.Admin.Validators.RegisterCommandValidator).Assembly);
        }

        private static void ConfigureAutoMapper(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(
                typeof(Finme.Application.Admin.Mappings.MappingProfile).Assembly);
        }

        private static void ConfigureAWS(WebApplicationBuilder builder)
        {
            var awsConfig = builder.Configuration.GetSection("AWS");
            var credentials = new BasicAWSCredentials(
                awsConfig["AccessKey"],
                awsConfig["SecretKey"]
            );
            var awsOptions = new AWSOptions
            {
                Credentials = credentials,
                Region = Amazon.RegionEndpoint.GetBySystemName(awsConfig["Region"])
            };

            builder.Services.AddDefaultAWSOptions(awsOptions);
            builder.Services.AddAWSService<IAmazonSimpleNotificationService>();
            builder.Services.AddAWSService<IAmazonSimpleEmailService>();
            builder.Services.AddAWSService<IAmazonS3>();
        }

        private static void ConfigureServices(WebApplicationBuilder builder, ILogger<Program> logger)
        {
            logger.LogInformation("Registering services...");
            builder.Services.AddScoped<IVerificationService, VerificationService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Configurar limites para upload de arquivos
            builder.Services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 10 * 1024 * 1024; // 50 MB
            });

            builder.Services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 50 MB
            });

            // Log serviços registrados
            logger.LogDebug("Registered services: IVerificationService, IJwtService, IRepository<>");
        }

        private static void ConfigureControllers(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
        }

        private static void ApplyDatabaseMigrations(WebApplication app, ILogger<Program> logger)
        {
            try
            {
                logger.LogInformation("Applying database migrations...");
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
                logger.LogInformation("Database migrations applied successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to apply database migrations: {Message}", ex.Message);
                throw;
            }
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseJwtMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}