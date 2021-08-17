using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PostMicroservice.FakeLogger
{
    interface IFakeLoggerRepository
    {
        public interface IFakeLoggerRepository
        {
            void Log(LogLevel logLevel, string requestId, string previousRequestId, string message, Exception exception);
        }
    }
}
