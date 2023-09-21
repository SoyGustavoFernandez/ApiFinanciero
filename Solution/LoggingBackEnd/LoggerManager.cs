using log4net;
using log4net.Config;

namespace LoggingBackEnd
{
    public class LoggerManager
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(LoggerManager));

        static LoggerManager()
        {
            XmlConfigurator.Configure();
        }

        public static void LogInfo(string message)
        {
            _log.Info(message);
        }

        public static void LogWarning(string message)
        {
            _log.Warn(message);
        }

        public static void LogError(string message)
        {
            _log.Error(message);
        }
    }
}