using Alchemy.BusinessLogic.Repositories;
using Alchemy.BusinessLogic.Services;
using Alchemy.DataModel.Entities;
using Alchemy.WebAPI.Models;
using Mapster;
using MapsterMapper;
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
