using System;
using System.IO;

namespace ShapeScape
{
    /// <summary>
    /// A static class providing methods to log messages to the console and a file.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// The path to the log file for this session.
        /// </summary>
        private static string logFilePath;

        /// <summary>
        /// Static constructor to initialize the log file for the session.
        /// </summary>
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
            logFilePath = Path.Combine(logDirectory, $"log_{timestamp}.log");
        }

        /// <summary>
        /// Logs the given message to the console and the log file.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Log(string message)
        {
            LogMessage("INFO", message);
        }
        /// <summary>
        /// Logs the given number to the console and the log file.
        /// </summary>
        /// <param name="message">The number to log.</param>
        public static void Log(double message)
        {
            LogMessage("INFO", ""+message);
        }
        /// <summary>
        /// Logs the given boolean to the console and the log file.
        /// </summary>
        /// <param name="message">The boolean to log.</param>
        public static void Log(bool message)
        {
            LogMessage("INFO", ""+message);
        }

        /// <summary>
        /// Logs the given error message to the console and the log file.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public static void LogError(string message)
        {
            LogMessage("ERROR", message);
        }

        /// <summary>
        /// Logs the given warning message to the console and the log file.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public static void LogWarning(string message)
        {
            LogMessage("WARNING", message);
        }

        /// <summary>
        /// Logs the given debug message to the console and the log file.
        /// </summary>
        /// <param name="message">The debug message to log.</param>
        public static void Debug(string message)
        {
            if (Developer.debugMode == false) return;
            LogMessage("DEBUG", message);
        }

        /// <summary>
        /// Logs the given message to the console and the log file.
        /// </summary>
        /// <param name="logLevel">The log level of the message.</param>
        /// <param name="message">The message to log.</param>
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

