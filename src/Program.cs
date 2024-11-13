using System;
using SquarePlatformer;

namespace SquarePlatformerApp
{
    public static class Program
    {
        public static void Main()
        {
            try
            {
                // Create and run the game instance
                using (var game = new Main())
                {
                    Console.WriteLine("Square Platformer is now starting...");
                    game.Run();
                    Console.WriteLine("Square Platformer has been closed.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                Console.Error.WriteLine(ex.StackTrace);
            }
        }
    }
}
