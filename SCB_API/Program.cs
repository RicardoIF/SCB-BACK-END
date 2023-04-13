using Contracts.Interfaces;
using Core.ActionFilters;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using NLog;
using SCB_API.Extensions;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

//Esta función local configura la compatibilidad con JSON Patch usando Newtonsoft.Json y deja los otros formateadores sin cambios.
NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
.Services.BuildServiceProvider()
.GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
.OfType<NewtonsoftJsonPatchInputFormatter>().First();

builder.Services.ConfigureCors();

builder.Services.ConfigureIISIntegration();

builder.Services.ConfigureLoggerService();

builder.Services.ConfigureRepositoryBase();

builder.Services.ConfigureServiceBase();

builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.ConfigureSwagger(builder.Configuration);

builder.Services.ConfigureAddControllerLoopJson();

builder.Services.ConfigureDataShaper();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ValidationFilterAttribute>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    //Suprime los valores de modelo por defecto de [ApiController]
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());//Funcion local 
})//.AddXmlDataContractSerializerFormatters() //Configuracion para poder obtener data en formato XML
  .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.UseCamelCasing(true);
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    })
  .AddApplicationPart(typeof(Core.Presentation.AssemblyReference).Assembly);//Referencia al proyecto Core.Presentation 


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "SCB.API");
    s.SwaggerEndpoint("/swagger/v2/swagger.json", "SCB.API");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
