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
            OldPlayer oldPlayer = new OldPlayer("test");
            oldPlayer.GetAttacked(50);
            Debug.Assert(oldPlayer.Health == 50);
            
            oldPlayer.PickUpItem(ItemType.Shield);
            Debug.Assert(oldPlayer.Inventory[ItemType.Shield] == 1);
            
            oldPlayer.StealItem();
            Debug.Assert(oldPlayer.Inventory[ItemType.Shield] == 0);
            
            oldPlayer.GiveBadLuck();
            Debug.Assert(oldPlayer.HasBadLuck);
            
            oldPlayer.PickUpItem(ItemType.HealthPotion);
            oldPlayer.UseItem(ItemType.HealthPotion);
            Debug.Assert(oldPlayer.Health > 50);
            Debug.Assert(oldPlayer.Inventory[ItemType.HealthPotion] == 0);
        }
    }
}