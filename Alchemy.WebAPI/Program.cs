using Alchemy.BusinessLogic.Repositories;
using Alchemy.BusinessLogic.Services;
using Alchemy.DataModel;
using Alchemy.Domain.Entities;
using Alchemy.Domain.Repositories;
using Alchemy.Domain.Services;
using Alchemy.WebAPI.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DataStore
var csvHelper = new CsvHelperService(builder.Configuration);
var dataTransform = new DataTransformService(csvHelper);
DataStore dataStore = await dataTransform.CreateDataStoreAsync();
builder.Services.AddSingleton(dataStore);

builder.Services.AddScoped<IRepository<DownloadableContent>, DownloadableContentRepository>();
builder.Services.AddScoped<IRepository<Ingredient>, IngredientsRepository>();
builder.Services.AddScoped<IPagedRepository<Effect>, EffectsRepository>();
builder.Services.AddScoped<IPagedRepository<Ingredient>, IngredientsRepository>();
builder.Services.AddScoped<IMixer, Mixer>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TES-V Alchemy WebService", Version = "v1" }));

// Cors policies
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder => policyBuilder
        .WithOrigins("https://alchemy.quicktortoise.com.mx")
        .WithMethods("GET"));
    options.AddPolicy("development", policyBuilder => policyBuilder
        .WithOrigins("http://localhost:4200")
        .WithMethods("GET"));
});

WebApplication app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure HTTP req. pipeline
if (app.Environment.IsDevelopment())
{
    app.UseCors("development");
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Alchemy.WebAPI v1"));

}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
