using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class OldPlayer
    {
        /// <summary>
        /// Name entered by player at start of the program.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Amount of health the player has. By default is 100.
        /// </summary>
        public int Health { get; private set; }

        /// <summary>
        /// Items in the player's inventory and their amounts.
        /// </summary>
        public Dictionary<ItemType, int> Inventory { get; } = new Dictionary<ItemType, int>();
        
        private bool _shieldEquipped = false;
        public bool HasBadLuck = false;

        public OldPlayer(string name) 
        {
            Name = name;
            Health = 100;
        }
        
        /// <summary>
        /// Returns contents of player's inventory and number of each item they have.
        /// </summary>
        /// <returns>The display string</returns>
        public string InventoryContents()
        {
            List<string> items = new List<string>();
            foreach (var inventoryItems in Inventory)
            {
                int itemCount = inventoryItems.Value;
                if (itemCount == 0)
                    continue;
                    
                ItemType itemType = inventoryItems.Key;
                items.Add($"{itemCount}x {itemType.ToString()}");
            }
            
            return string.Join(", ", items);
        }
        
        /// <summary>
        /// Picks up item and adds it to player's inventoryu
        /// </summary>
        /// <param name="item">Type of item being added to player's inventory.</param>
        public void PickUpItem(ItemType item)
        {
            if (Inventory.ContainsKey(item))
            {
                Inventory[item] += 1;
                return;
            }
            
            Inventory.Add(item, 1);
        }

        /// <returns>True if the player has the item type in their inventory</returns>
        public bool HasItem(ItemType item)
        {
            return Inventory.ContainsKey(item) && Inventory[item] != 0;
        }

        /// <summary>
        /// Use a particular item in the player's inventory
        /// </summary>
        /// <param name="item">The type of item being used</param>
        public void UseItem(ItemType item)
        {
            switch (item)
            {
                case ItemType.HealthPotion:
                    AddHealth(30);
                    break;
                case ItemType.Shield:
                    if (_shieldEquipped)
                    {
                        Console.WriteLine("You already have a shield equipped. Equip another shield when the currently equipped shield is used.");
                        return;
                    }

                    _shieldEquipped = true;
                    break;
                case ItemType.EscapeCode:
                    Console.WriteLine("You have escaped the dungeon and won the game, congratulations!");
                    Game.Quit();
                    break;
            }

            Inventory[item] -= 1;
        }
        
        /// <summary>
        /// Removes health from player
        /// </summary>
        /// <param name="amount">Amount of health to remove</param>
        private void RemoveHealth(int amount)
        {
            if (Health - amount < 0)
                Health = 0;
            else
                Health -= amount;
            
            Console.WriteLine($"You lost {amount} health. Your health is now {Health}%.");

            if (Health == 0)
            {
                Console.WriteLine("You died, better luck next time.");
                Game.Quit();
            }
        }

        /// <summary>
        /// Adds health to player
        /// </summary>
        /// <param name="amount">Amount of health to add</param>
        private void AddHealth(int amount)
        {
            if (Health + amount > 100)
                Health = 100;
            else
                Health += amount;
            
            Console.WriteLine($"Your health is now {Health}%.");
        }

        /// <summary>
        /// Get health removed due to attack from monster. Only removed if player does not have shield equipped. Otherwise, player is notified about this.
        /// </summary>
        /// <param name="healthLoss">Health loss as a result of the attack</param>
        public void GetAttacked(int healthLoss)
        {
            if (_shieldEquipped)
            {
                Console.WriteLine("You were attacked but your shield saved you from damage. You no longer have a shield equipped.");
                _shieldEquipped = false;
                return;
            }
            
            RemoveHealth(healthLoss);
        }

        /// <summary>
        /// Gives player higher odds of encountering a monster in the next room
        /// </summary>
        public void GiveBadLuck()
        {
            HasBadLuck = true;
            Console.WriteLine($"You have been given bad luck.");
        }
        
        /// <summary>
        /// Removes an item from the player's inventory if they have one and notifies them
        /// </summary>
        public void StealItem()
        {
            ItemType? stolenItem = null;
            
            // Finds first item in the player's inventory that has at least a count of 1, removes if exists.
            foreach (var inventoryItem in Inventory)
            {
                ItemType itemType = inventoryItem.Key;
                int count = inventoryItem.Value;

                if (count == 0)
                    continue;
                
                Inventory[itemType] -= 1;
                stolenItem = itemType;
                break;
            }

            // If player has no items (e.g all counts were zero, or none existent, thief cannot steal)
            if (stolenItem == null)
            {
                Console.WriteLine("The thief attempted to steal from you however you had no items in your inventory.");
                return;
            }
            
            Console.WriteLine($"The thief stole 1x {stolenItem} from you.");
        }
    }
}