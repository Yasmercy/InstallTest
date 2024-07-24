using NLog;

namespace MyApp
{
    static class LogSet
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogError(string message)
            => logger.Error(message);
        public static void LogInfo(string message)
            => logger.Info(message);
        public static void LogDebug(string message)
            => logger.Debug(message);
        public static void LogTrace(string message)
            => logger.Trace(message);
    }
}
