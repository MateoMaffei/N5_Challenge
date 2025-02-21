
using Cronos;
using N5_Challenge.Configuration.Options;
using N5_Challenge.Services.Interface;

namespace N5_Challenge.Worker
{
    public class AprobarPermisosWorker : BackgroundService
    {
        private readonly ILogger<AprobarPermisosWorker> _logger;
        private readonly IPermisoServices _permisoServices;
        private readonly WorkerOptions _workerOptions;
        private DateTime _nextRun;

        public AprobarPermisosWorker(ILogger<AprobarPermisosWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _nextRun = DateTime.UtcNow;
            var scope = serviceProvider.CreateScope();
            _permisoServices = scope.ServiceProvider.GetService<IPermisoServices>()!;
            _workerOptions = scope.ServiceProvider.GetService<WorkerOptions>()!;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cronExpression = CronExpression.Parse(_workerOptions.AprobarPermisoCron);

            while (!stoppingToken.IsCancellationRequested)
            {
                _nextRun = cronExpression.GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Utc) ?? DateTime.UtcNow.AddMinutes(1);

                var delay = _nextRun - DateTime.UtcNow;
                _logger.LogInformation($"Próxima ejecución programada para: {_nextRun}");

                if (delay > TimeSpan.Zero)
                    await Task.Delay(delay, stoppingToken);

                _logger.LogInformation("Inicia [AprobarPermisosAsync]");
                await _permisoServices.AprobarPermisosAsync();
                _logger.LogInformation("Finaliza [AprobarPermisosAsync]");
            }
        }
    }
}
