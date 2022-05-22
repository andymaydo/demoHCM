using SendMailWs.Interfaces;

namespace SendMailWs
{
    public class Worker : BackgroundService
    {
        private readonly IHost _host;
        private readonly ILogger<Worker> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISqlLog _sqlLogger;
        

        public Worker(ILogger<Worker> logger, IHost host, IUnitOfWork unitOfWork, ISqlLog sqlLogger)
        {
            _host = host;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _sqlLogger = sqlLogger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            try
            {
                await _unitOfWork.ProcessMailQueue();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await _sqlLogger.AddSqlLog("2000", ex.Message, ex);
            }
            finally
            {
                _host.StopAsync();
            }
            
        }
    }
}
