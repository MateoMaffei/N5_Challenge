using N5_Challenge.Repositories.Interfaces;
using N5_Challenge.Repositories;
using N5_Challenge.Services.Interface;
using N5_Challenge.Services;
using Entities.DbContexts;
using Microsoft.EntityFrameworkCore;
using N5_Challenge.Worker;

namespace N5_Challenge.Configuration
{
    public static class ApiConfiguration
    {
        public static void AddConfigurationApp(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddSettings();
            builder.Services.AddServices();
            builder.Services.AddRepositories();
            builder.Services.AddWorkers();
            builder.AddContext();
        }

        public static void AddSettings(this IConfigurationBuilder configuration)
        {
            var configurationbuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();

            configuration.AddConfiguration(configurationbuilder.Build());
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPermisoServices, PermisoServices>();
            services.AddScoped<IElasticService, ElasticService>();
            services.AddScoped<IKafkaProducerServices, KafkaProducerServices>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPermisoRepository, PermisoRepository>();
            services.AddScoped<ITipoPermisoRepository, TipoPermisoRepository>();
            services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
        }

        public static void AddContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(x =>
                x.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetSection("Application:ConnectionString").Value));
        }

        public static void AddWorkers(this IServiceCollection services)
        {
            services.AddHostedService<AprobarPermisosWorker>();
        }
    }
}
