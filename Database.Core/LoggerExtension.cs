using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Database.Core
{
    public static class LoggerExtension
    {
        public static void LogTraceStart(this ILogger logger, [CallerMemberName] string message = "")
        {
            logger.Log(LogLevel.Trace,"Start " + message);
        }

        public static void LogTraceEnd(this ILogger logger, [CallerMemberName] string message = "")
        {
            logger.Log(LogLevel.Trace, "End " + message);
        }
    }
}
