using System;
using System.Diagnostics;
using System.Threading;
using NLog.Common;

namespace NLogTest
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                InternalLogger.LogLevel = NLog.LogLevel.Error;
                InternalLogger.LogMessageReceived += InternalLogger_LogMessageReceived;
                for (int i = 0; i < 100; i++)
                {
                    logger.Info($"this is {i + 1} line");
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static bool flag = false;

        private static void InternalLogger_LogMessageReceived(object sender, InternalLoggerMessageEventArgs e)
        {
            if (!flag)
            {
                NLogEventLog lisaEventLog = new NLogEventLog();
                var process = Process.GetCurrentProcess();
                lisaEventLog.WriteEntry($"ProcessId = {process.Id}, {process.SessionId}", EventLogEntryType.Error);
            }
            var exception = e.Exception;
            if (exception != null)
            {
                var content = $"{e.Message}{Environment.NewLine},{e.Exception}";
                NLogEventLog lisaEventLog = new NLogEventLog();
                lisaEventLog.WriteEntry(content, EventLogEntryType.Error);
            }
        }
    }
}
