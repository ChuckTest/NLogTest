using System;
using System.Diagnostics;
using NLog;
using NLog.Common;
using NLog.Targets.ElasticSearch;
using NLog.Targets.Wrappers;

namespace NLogTest
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                InternalLogger.LogLevel = LogLevel.Trace;
                InternalLogger.LogMessageReceived += InternalLogger_LogMessageReceived;

                var targetName = "elastic";
                var target = LogManager.Configuration.FindTargetByName(targetName);
                if (target is BufferingTargetWrapper bufferingTargetWrapper)
                {
                    if (bufferingTargetWrapper.WrappedTarget is ElasticSearchTarget elasticSearchTarget)
                    {
                        Console.WriteLine(elasticSearchTarget.Uri);
                        Console.WriteLine(elasticSearchTarget.Index);
                        //elasticSearchTarget.Uri = "http://172.31.211.17:9200/";
                        //elasticSearchTarget.Index = "logstash-20200708-001";
                    }
                }

                logger.Info($"this is a test log {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff zzz}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void InternalLogger_LogMessageReceived(object sender, InternalLoggerMessageEventArgs e)
        {
            var exception = e.Exception;
            if (exception != null && e.Level >= LogLevel.Error)
            {
                var content = $"{e.Message}{Environment.NewLine},{e.Exception}";
                NLogEventLog lisaEventLog = new NLogEventLog();
                lisaEventLog.WriteEntry(content, EventLogEntryType.Error);
            }
        }
    }
}
