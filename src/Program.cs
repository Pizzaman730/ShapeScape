using System;
using System.Runtime.InteropServices;

namespace ShapeScape
{
    public static class Program
    {
        public static void Main()
        {
            // Log basic application start
            Logger.Log("Game Loading....");

            // Log system and environment details
            LogSystemInfo();
            
            try
            {
                // Create and run the game instance
                using (var game = new Main())
                {
                    Logger.Log("Loaded!");
                    game.Run();
                    Logger.Log("Game closing.");
                }
            }
            catch (Exception ex)
            {
                // Log error message if an exception occurs
                Logger.LogError($"An error occurred: {ex.Message}");
                Logger.LogError($"Stack Trace: {ex.StackTrace}");
            }
            finally
            {
                Logger.Log("Game has closed.");
            }
        }

        // Helper method to log system and app information
        private static void LogSystemInfo()
        {
            Logger.Log($"OS: {RuntimeInformation.OSDescription}");
            Logger.Log($"Architecture: {RuntimeInformation.OSArchitecture}");
            Logger.Log($"Framework: {RuntimeInformation.FrameworkDescription}");
            Logger.Log($"Machine Name: {Environment.MachineName}");
            Logger.Log($"User Name: {Environment.UserName}");
            Logger.Log($"App Version: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}");
            Logger.Log($"App Directory: {AppDomain.CurrentDomain.BaseDirectory}");
            Logger.Log($"Date/Time Started: {DateTime.Now}");
        }
    }
}
