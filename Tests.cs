using System;
using System.IO;

namespace DungeonExplorer
{
    public static class PlayerTests
    {
        public static string LogFilePath = "./tests-log.txt";
        
        /// <summary>
        /// Tests player methods to ensure they are working correctly
        /// </summary>
        public static void TestPlayer()
        {
            Player player = new Player("test");
            player.TakeDamage(50);
            if (player.Health != 50)
            {
                LogFailed("Health was not removed correctly");
                return;
            }

            var healthPotion = new HealthPotion();
            player.Inventory.PickupItem(healthPotion);

            if (player.Inventory.Contents[healthPotion.Name].Item2 != 1)
            {
                LogFailed("First inventory item not counted correctly");
                return;
            }
            
            player.Inventory.PickupItem(healthPotion);

            if (player.Inventory.Contents[healthPotion.Name].Item2 != 2)
            {
                LogFailed("Second inventory item not counted correctly");
                return;
            }
            
            player.Inventory.UseItem(healthPotion.Name);

            if (player.Inventory.Contents[healthPotion.Name].Item2 != 1)
            {
                LogFailed("Inventory item not decremented after being used");
                return;
            }
            
            
            healthPotion.ApplyTo(player);
            if (player.Health != 50 + healthPotion.HealthBonus)
            {
                LogFailed("Health Potion not Applying Health Correctly");
                return;
            }
            
            using (StreamWriter writer = new StreamWriter(LogFilePath, append: true))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Tests Passed");
            }
            
            Console.WriteLine("Tests Passed.");
        }

        public static void LogFailed(string reason)
        {
            Console.WriteLine($"Tests Failed ({reason})");
            
            using (StreamWriter writer = new StreamWriter(LogFilePath, append: true))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Tests Failed ({reason})");
            }
        }
    }
}