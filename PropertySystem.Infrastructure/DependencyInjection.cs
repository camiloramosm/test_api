using PropertySystem.Application.Abstractions.Authentication;
using PropertySystem.Application.Abstractions.Clock;
using PropertySystem.Application.Abstractions.Data;
using PropertySystem.Application.Abstractions.Email;
using PropertySystem.Infrastructure.Authentication;
using PropertySystem.Infrastructure.Authorization;
using PropertySystem.Infrastructure.Clock;
using PropertySystem.Infrastructure.Data;
using PropertySystem.Infrastructure.Email;
using PropertySystem.Infrastructure.Repositories;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using AuthenticationOptions = PropertySystem.Infrastructure.Authentication.AuthenticationOptions;
using AuthenticationService = PropertySystem.Infrastructure.Authentication.AuthenticationService;
using IAuthenticationService = PropertySystem.Application.Abstractions.Authentication.IAuthenticationService;
using PropertySystem.Domain.Properties;
using PropertySystem.Domain.Users;
using PropertySystem.Domain.Abstractions;
using Dapper;

namespace PropertySystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        AddPersistence(services, configuration);

        AddAuthentication(services, configuration);

        AddAuthorization(services);

        return services;
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services
          .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer();

        var rest = configuration.GetSection("Authentication");

        services.Configure<AuthenticationOptions>(rest);

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

        services.AddTransient<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
        {
            KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
        })
       .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
        {
            KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
        });

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException(nameof(configuration));
        var mongoConnectionString = configuration.GetConnectionString("MongoConnection") ?? throw new ArgumentException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString).UseSnakeCaseNamingConvention();
        });

        // MongoDB Configuration
        services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoConnectionString));
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase("PropertySystem");
        });
        services.AddScoped<MongoDbContext>();

        // SQL Server Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // MongoDB Repositories
        services.AddScoped<IPropertyRepository, MongoPropertyRepository>();
        services.AddScoped<IOwnerRepository, MongoOwnerRepository>();

        services.AddScoped<IUnitOfWork, ApplicationDbContext>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
    }

    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddScoped<AuthorizationService>();
        services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();
    }

}
