using MovieRS.Video.Core.Contracts;
using MovieRS.Video.Core.Repositories;

namespace MovieRS.Video.Extensions
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

        public static void ConfigureRepository(this IServiceCollection services) => services
            .AddScoped<IUnitOfWork, UnitOfWork>();

        public static void ConfigureTunnel(this IServiceCollection services) => services
            .AddSingleton<IUpdateDomain, UpdateDomainExtension>();
    }
}
