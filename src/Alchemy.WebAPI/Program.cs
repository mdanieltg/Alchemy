using Alchemy.DataModel;
using Alchemy.WebAPI.Extensions;
using Alchemy.WebAPI.Services;
using Microsoft.AspNetCore.HttpOverrides;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

CsvHelperService csvHelper = new(builder.Configuration);
DataTransformService dataTransform = new(csvHelper);
DataStore dataStore = await dataTransform.CreateDataStoreAsync();

// Services
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true);

builder.Services
    .AddMapper()
    .AddSingleton(dataStore)
    .AddRepositories()
    .AddServices()
    .AddSwagger()
    .SetupCors(builder.Configuration);

WebApplication app = builder.Build();

// Configure HTTP req. pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Alchemy.WebAPI v1"));
    app.UseCors("development");
}
else
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    app.UseCors();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
