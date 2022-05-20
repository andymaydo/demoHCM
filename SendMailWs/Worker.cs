using HCMDataAccess;
using HCMModels;
using SendMailWs.Interfaces;

namespace SendMailWs
{
    public class Worker : BackgroundService
    {
        private readonly IHost _host;
        private readonly ILogger<Worker> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public Worker(ILogger<Worker> logger, IHost host, IUnitOfWork unitOfWork)
        {
            _host = host;
            _logger = logger;
            _unitOfWork = unitOfWork;
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
            }
            finally
            {
                _host.StopAsync();
            }
            
        }
    }
}
