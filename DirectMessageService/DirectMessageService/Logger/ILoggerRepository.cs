using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.Logger
{
    public interface ILoggerRepository<T>
    {
        void LogError(Exception ex, string message, params object[] args);

        void LogInformation(string message);
    }
}
