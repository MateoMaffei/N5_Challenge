using N5_Challenge.Configuration.Options;

namespace N5_Challenge.Configuration
{
    public static class OptionsConfiguration
    {
        public static void AddApplicationOptions(this IServiceCollection services)
        {
            IConfiguration configuration;

            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                configuration = serviceScope.ServiceProvider.GetService<IConfiguration>()!;
            }

            services.AddAppOptions(configuration);
            services.AddElasticOptions(configuration);
            services.AddKafkaOptions(configuration);
            services.AddWorkerOptions(configuration);
        }

        public static void AddAppOptions(this IServiceCollection services, IConfiguration config)
        {
            var appOptions = new AppOptions();
            config.GetSection(AppOptions.Section).Bind(appOptions);
            services.AddSingleton(typeof(AppOptions), appOptions);
        }

        public static void AddElasticOptions(this IServiceCollection services, IConfiguration config)
        {
            var elasticOptions = new ElasticSearchOptions();
            config.GetSection(ElasticSearchOptions.Section).Bind(elasticOptions);
            services.AddSingleton(typeof(ElasticSearchOptions), elasticOptions);
        }

        public static void AddKafkaOptions(this IServiceCollection services, IConfiguration config)
        {
            var kafkaOptions = new KafkaOptions();
            config.GetSection(KafkaOptions.Section).Bind(kafkaOptions);
            services.AddSingleton(typeof(KafkaOptions), kafkaOptions);
        }

        public static void AddWorkerOptions(this IServiceCollection services, IConfiguration config)
        {
            var workerOptions = new WorkerOptions();
            config.GetSection(WorkerOptions.Section).Bind(workerOptions);
            services.AddSingleton(typeof(WorkerOptions), workerOptions);
        }
    }
}
