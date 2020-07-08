using System;
using System.Diagnostics;
using NUnit.Framework;

namespace NLogTest
{
    [TestFixture]
    public class ChuckTest
    {
        [Test]
        public void Test()
        {
            try
            {
                NLogEventLog lisaEventLog = new NLogEventLog();
                var process = Process.GetCurrentProcess();
                lisaEventLog.WriteEntry($"ProcessId = {process.Id}, {process.SessionId}", EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
