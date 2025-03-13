using System.Diagnostics;

namespace DungeonExplorer
{
    public static class PlayerTests
    {
        /// <summary>
        /// Tests player methods to ensure they are working correctly
        /// </summary>
        public static void TestPlayer()
        {
            Player player = new Player("test");
            player.GetAttacked(50);
            Debug.Assert(player.Health == 50);
            
            player.PickUpItem(ItemType.Shield);
            Debug.Assert(player.Inventory[ItemType.Shield] == 1);
            
            player.StealItem();
            Debug.Assert(player.Inventory[ItemType.Shield] == 0);
            
            player.GiveBadLuck();
            Debug.Assert(player.HasBadLuck);
            
            player.PickUpItem(ItemType.HealthPotion);
            player.UseItem(ItemType.HealthPotion);
            Debug.Assert(player.Health > 50);
            Debug.Assert(player.Inventory[ItemType.HealthPotion] == 0);
        }
    }
}