using System;
using System.IO;

namespace SquarePlatformer
{
    public static class Logger
    {
        private static string logFilePath;

        // Static constructor to initialize the log file for the session
        static Logger()
        {
            // Ensure the /logs/ directory exists
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Create a log file with a timestamp
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            logFilePath = Path.Combine(logDirectory, $"log_{timestamp}.txt");
        }

        public static void Log(string message)
        {
            LogMessage("INFO", message);
        }
        public static void Log(double message)
        {
            LogMessage("INFO", ""+message);
        }
        public static void Log(bool message)
        {
            LogMessage("INFO", ""+message);
        }

        public static void LogError(string message)
        {
            LogMessage("ERROR", message);
        }

        public static void LogWarning(string message)
        {
            LogMessage("WARNING", message);
        }

        private static void LogMessage(string logLevel, string message)
        {
            Console.WriteLine($"{logLevel}: {message}");
            try
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string logMessage = $"{timestamp} [{logLevel}] {message}";

                // Append to the log file
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Logging failed: {ex.Message}");
            }
        }
    }
}
