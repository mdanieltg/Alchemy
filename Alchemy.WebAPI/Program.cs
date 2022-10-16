using Alchemy.BusinessLogic.Contracts;
using Alchemy.BusinessLogic.Services;
using Alchemy.DataModel;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
    .AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TES-V Alchemy WebService", Version = "v1" }));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var csv = new Alchemy.DataModel.CsvHelper(builder.Configuration);
var context = await AlchemyContextFactory.CreateContext(csv);
builder.Services.AddSingleton(context);
builder.Services.AddScoped<IDlcRepository, DlcRepository>();
builder.Services.AddScoped<IEffectsRepository, EffectsRepository>();
builder.Services.AddScoped<IIngredientsRepository, IngredientsRepository>();
builder.Services.AddScoped<IMixer, Mixer>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure HTTP req. pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Alchemy.WebAPI v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
