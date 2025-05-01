using System;
using System.Diagnostics;

namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Executes tests if the debugger is being used
            if (Debugger.IsAttached)
            {
                Console.WriteLine("Running Tests....");
                PlayerTests.TestPlayer();
                Console.WriteLine("==============");
                Console.WriteLine();
            }
            
            Game game = new Game();
            game.Start();
        }
    }
}
