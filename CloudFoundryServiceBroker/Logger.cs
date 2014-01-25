using System.Diagnostics;

namespace CloudFoundryServiceBroker
{
    public static class Logger
    {
        public const string EventSource = "MsSqlCloudFoundryService";

        public static void Debug(string message)
        {
#if DEBUG
            WriteEntry(EventLogEntryType.SuccessAudit, message);
#endif
        }
        public static void DebugFormat(string logMessage, params object[] parameters)
        {
#if DEBUG
            WriteEntry(EventLogEntryType.SuccessAudit, logMessage, parameters);
#endif
        }

        public static void Info(string message)
        {
            WriteEntry(EventLogEntryType.Information, message);
        }
        public static void InfoFormat(string logMessage, params object[] parameters)
        {
            WriteEntry(EventLogEntryType.Information, logMessage, parameters);
        }

        public static void Warning(string message)
        {
            WriteEntry(EventLogEntryType.Warning, message);
        }
        public static void WarningFormat(string logMessage, params object[] parameters)
        {
            WriteEntry(EventLogEntryType.Warning, logMessage, parameters);
        }

        public static void Error(string message)
        {
            WriteEntry(EventLogEntryType.Error, message);
        }
        public static void ErrorFormat(string logMessage, params object[] parameters)
        {
            WriteEntry(EventLogEntryType.Error, logMessage, parameters);
        }

        private static void WriteEntry(EventLogEntryType type, string logMessage, params object[] parameters)
        {
            string message = logMessage;
            if (parameters != null && parameters.Length != 0) message = string.Format(logMessage, parameters);

            if (!EventLog.SourceExists(EventSource))
            {
                EventLog.CreateEventSource(EventSource, EventSource);
            }
            EventLog.WriteEntry(EventSource, message, type);
        }
    }
}
