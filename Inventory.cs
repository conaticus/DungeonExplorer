using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Inventory
    {
        public Dictionary<String, (Item, int)> Contents { get; } = new Dictionary<String, (Item, int)>();

        public bool IsShieldEquipped = false;
        public Weapon EquippedWeapon = null;
        
        /// <summary>
        /// Get string of inventory contents.
        /// </summary>
        /// <returns>String of all items in the player's inventory</returns>
        public string InventoryContents()
        {
            List<string> items = new List<string>();
            foreach (var itemEntry in Contents)
            {
                var (item, itemCount) = itemEntry.Value;
                
                if (itemCount == 0)
                    continue;
                    
                items.Add($"{itemCount}x {item.Name}");
            }
            
            return string.Join(", ", items);
        }
        
        /// <summary>
        /// Adds item to inventory
        /// </summary>
        /// <param name="item">Item to add to inventory</param>
        public void PickupItem(Item item)
        {
            if (Contents.TryGetValue(item.Name, out var itemTuple))
            {
                Contents[item.Name] = (itemTuple.Item1, itemTuple.Item2 + 1);
                return;
            }
            
            Contents.Add(item.Name, (item, 1));
        }

        /// <summary>
        /// Returns item in the inventory by name
        /// </summary>
        /// <param name="name">Entity name of the item to search for</param>
        /// <returns>Item class that is associated with that name, if inside inventory</returns>
        public Item GetItemByName(String name)
        {
            return Contents[name].Item1;
        }
        
        /// <summary>
        /// Uses an item and removes it from the inventory as it has been used up
        /// </summary>
        /// <param name="itemName">Name of the item that has been used</param>
        public void UseItem(String itemName)
        {
            var itemTuple = Contents[itemName];
            var updatedTuple = (itemTuple.Item1, itemTuple.Item2 - 1);
            Contents[itemName] = updatedTuple;
            
            if (updatedTuple.Item2 == 0)
                Contents.Remove(itemTuple.Item1.Name);
        }

        /// <summary>
        /// Whether or not item is in player's inventory
        /// </summary>
        /// <param name="item">Item type to check for</param>
        /// <returns>Boolean of whether the item is in the inventory or  not</returns>
        public bool HasItem(Item item)
        {
            return Contents.ContainsKey(item.Name);
        }
    }
}