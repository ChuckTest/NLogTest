using System;
using System.Diagnostics;

namespace NLogTest
{
    public class NLogEventLog
    {
        private readonly string _logName = @"NLog";

        public string LogName => _logName;

        public NLogEventLog()
        {
        }

        public NLogEventLog(string logName)
        {
            _logName = logName;
        }

        public void WriteEntry(string error, EventLogEntryType type)
        {
            var sourceName = AppDomain.CurrentDomain.FriendlyName;
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, _logName);
            }

            using (EventLog eventLog = new EventLog(_logName))
            {
                eventLog.Source = sourceName;
                var message = $"{AppDomain.CurrentDomain.BaseDirectory}{Environment.NewLine}{error}";
                eventLog.WriteEntry(message, type);
            }
        }
    }
}
