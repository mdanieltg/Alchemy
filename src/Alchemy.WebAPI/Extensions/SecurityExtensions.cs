namespace Alchemy.WebAPI.Extensions;

public static class SecurityExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection SetupCors(IConfiguration configuration)
        {
            string[] origins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                               ?? throw new InvalidOperationException("Cors:AllowedOrigins not found in configuration");

            return services.AddCors(options =>
            {
                options.AddDefaultPolicy(policyBuilder => policyBuilder
                    .WithOrigins(origins)
                    .WithMethods("GET", "HEAD"));
            });
        }
    }
}
