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
                for (int i = 0; i < 30; i++)
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

        private static void InternalLogger_LogMessageReceived(object sender, InternalLoggerMessageEventArgs e)
        {
            var exception = e.Exception;
            if (exception != null && e.Level >= NLog.LogLevel.Error)
            {
                var content = $"{e.Message}{Environment.NewLine},{e.Exception}";
                NLogEventLog lisaEventLog = new NLogEventLog();
                lisaEventLog.WriteEntry(content, EventLogEntryType.Error);
            }
        }
    }
}
