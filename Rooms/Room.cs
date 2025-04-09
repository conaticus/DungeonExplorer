using System;

namespace DungeonExplorer
{
    public class Room
    {
        public string Description { get; }
        
        // Only monster or item will be populated, not both
        // Both can also be null
        public MonsterType? Monster { get; }
        public ItemType? Item { get; }

        /// <summary>
        /// Uses random to generate items/monsters that are found in this particular room
        /// </summary>
        /// <param name="player">Player entering the room</param>
        public Room(Player player)
        {
            Random random = new Random();
            Description = Config.RoomDescriptions[random.Next(0, Config.RoomDescriptions.Count)];

            // Chooses number between 1, 10
            int result = random.Next(1, 11);

            int monsterSpawnThreshold = player.HasBadLuck ? 5 : 8; // Bad luck: 50% chance of monster spawning, otherwise 30% chance (e.g 8,9,10 - 30%)
            int itemSpawnThreshold = player.HasBadLuck ? 3 : 5; // Bad luck: 20% chance of item spawning (e.g 3,4 - 20%), otherwise 30% chance
            
            if (result >= monsterSpawnThreshold)
                Monster = (MonsterType) random.Next(0, Enum.GetValues(typeof(MonsterType)).Length);
            else if (result >= itemSpawnThreshold)
            {
                // Re-initialize random so that it is as random as possible
                random = new Random();
                
                // Chooses number between 1, 10
                result = random.Next(1, 11);
                
                // 1/10 chance of getting escape code, if also spawns an item in that room
                if (result == 10)
                {
                    Item = ItemType.EscapeCode;
                    return;
                }
                
                result = random.Next(0, 2); // 50/50 chance of getting either shield or health potion
                if (result == 0)
                    Item = ItemType.Shield;
                else
                    Item = ItemType.HealthPotion;
            }
        }
    }
}