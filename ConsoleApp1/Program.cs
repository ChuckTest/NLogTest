using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            try
            {
                string fileName = "NLogTest.exe";
                int number = 100;
                for (int i = 0; i < number; i++)
                {
                    ProcessHelper.Start(fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }

    class ProcessHelper
    {
        public static void Start(string fileName)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                CreateNoWindow = true, 
                UseShellExecute = false, 
                FileName = fileName
            };
            var process = Process.Start(processStartInfo);
            if (process == null)
            {
                Console.WriteLine("Process start failed");
            }
            else
            {
                if (process.HasExited)
                {
                    Console.WriteLine($"process.HasExited = {process.HasExited}, process.ExitCode = {process.ExitCode}");
                }
                else
                {
                    Console.WriteLine($"process.HasExited = {process.HasExited}");
                }
            }
        }
    }
}
