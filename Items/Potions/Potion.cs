using System;

namespace DungeonExplorer
{
    public class Potion : Item
    {
        public int HealthBonus;
        
        public Potion(String name, int healthBonus) : base(name)
        {
            HealthBonus = healthBonus;
        }

        /// <summary>
        /// Apply all potion effects to a specific player
        /// </summary>
        /// <param name="player">The player to apply the effects to</param>
        public void ApplyTo(Player player)
        {
            player.AddHealth(HealthBonus);
        }
    }
}