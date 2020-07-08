using System;

namespace NLogTest
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                for (int i = 0; i < 30; i++)
                {
                    logger.Info($"this is {i + 1} line");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
