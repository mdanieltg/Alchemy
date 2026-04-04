using System.Threading.RateLimiting;
using Alchemy.Application.Repositories;
using Alchemy.Application.Services;
using Alchemy.DataModel.Entities;
using Alchemy.WebAPI.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi;

namespace Alchemy.WebAPI.Extensions;

public static class ConfigurationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddRepositories()
        {
            return services
                .AddScoped<IRepository<DownloadableContent>, DownloadableContentRepository>()
                .AddScoped<IRepository<Ingredient>, IngredientsRepository>()
                .AddScoped<IPagedRepository<Effect>, EffectsRepository>()
                .AddScoped<IPagedRepository<Ingredient>, IngredientsRepository>();
        }

        public IServiceCollection AddServices()
        {
            return services.AddScoped<IMixer, Mixer>();
        }

        public IServiceCollection AddMapper()
        {
            TypeAdapterConfig config = new();
            config.NewConfig<Ingredient, IngredientDto>()
                .Map(dto => dto.Dlc, ingredient => ingredient.Dlc != null ? ingredient.Dlc.Name : null);

            return services
                .AddSingleton(config)
                .AddScoped<IMapper, ServiceMapper>();
        }

        public IServiceCollection SetUpRateLimiter()
        {
            return services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddFixedWindowLimiter("fixed", limiterOptions =>
                {
                    limiterOptions.PermitLimit = 10;
                    limiterOptions.Window = TimeSpan.FromSeconds(30);
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    limiterOptions.QueueLimit = 5;
                });
            });
        }

        public IServiceCollection AddSwagger()
        {
            return services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(c =>
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "TES-V Alchemy WebService",
                        Version = "v1"
                    }));
        }
    }
}
