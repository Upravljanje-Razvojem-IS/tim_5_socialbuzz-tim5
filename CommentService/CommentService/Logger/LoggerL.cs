using Microsoft.Extensions.Logging;

namespace CommentService.Logger
{
    public class LoggerL
    {
        private readonly ILogger<LoggerL> _logger;

        public LoggerL(ILogger<LoggerL> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
