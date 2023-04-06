using Microsoft.OpenApi.Models;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Core.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieRS.API.Models;
using Microsoft.ML;

namespace MovieRS.API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            policy.SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader().AllowCredentials());
        });

        public static void ConfigureRepositorysitory(this IServiceCollection services) => services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddSingleton<MLContext, MLContext>();

        public static void ConfigureExternalAPI(this IServiceCollection services) =>
            services.AddScoped<ITMDb, TMDbAPI>();

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });
        }

        public static void ConfigureSwaggerOptions(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer",
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
                }
            });

        }
    }
}
