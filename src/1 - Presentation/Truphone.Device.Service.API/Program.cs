using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using System.Reflection;
using Truphone.Device.Service.CC.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInternalApplication()
    .AddInternalDomainServices()
    .AddInternalRepositories();

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
}).AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    x.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => { x.SwaggerEndpoint("v1/swagger.yaml", "Truphone Device API"); });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
{
    var builder = new ServiceCollection()
        .AddLogging()
        .AddMvc()
        .AddNewtonsoftJson()
        .Services.BuildServiceProvider();

    return builder
        .GetRequiredService<IOptions<MvcOptions>>()
        .Value
        .InputFormatters
        .OfType<NewtonsoftJsonPatchInputFormatter>()
        .First();
}