namespace SCB_API.Extensions
{
    using Contracts.Interfaces;
    using Contracts.Interfaces.DataShaper;
    using Contracts.Interfaces.RepositoryBase;
    using LoggerService.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.OpenApi.Models;
    using Repository.Context;
    using Repository.RepositoryBase;
    using Service.Contracts.ServiceBase;
    using Service.DataShaping;
    using Service.ServiceBase;
    using System.Text.Json.Serialization;

    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });

        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sistemas de Cubículos Bibliotecarios (SCB)",
                    Version = "v1",
                    Description = "API de MicroServicio Para la Reservación de Cubículos Bibliotecarios"
                });
                s.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Sistemas de Cubículos Bibliotecarios (SCB)",
                    Version = "v2",
                    Description = "API de MicroServicio Para la Reservación de Cubículos Bibliotecarios"
                });
                s.AddServer(new OpenApiServer()
                {
                    Url = configuration["Swagger:ServerUrl"]
                });
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        

        

        public static void ConfigureRepositoryBase(this IServiceCollection services) =>
           services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

        public static void ConfigureServiceBase(this IServiceCollection services) =>
          services.AddScoped(typeof(IServiceBase<,,,>), typeof(ServiceBase<,,,>));

        public static void ConfigureDataShaper(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDataShaper<>), typeof(DataShaper<>));
        }

        

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureAddControllerLoopJson(this IServiceCollection services) =>
            services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
    }
}
