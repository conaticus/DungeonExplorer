using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Inventory
    {
        public Dictionary<ItemType, int> Contents { get; } = new Dictionary<ItemType, int>();
        
        public string InventoryContents()
        {
            List<string> items = new List<string>();
            foreach (var item in Contents)
            {
                int itemCount = item.Value;
                if (itemCount == 0)
                    continue;
                    
                ItemType itemType = item.Key;
                items.Add($"{itemCount}x {itemType.ToString()}");
            }
            
            return string.Join(", ", items);
        }
        
        public void PickupItem(ItemType item)
        {
            if (Contents.ContainsKey(item))
            {
                Contents[item] += 1;
                return;
            }
            
            Contents.Add(item, 1);
        }
        
        /// <returns>True if the item is present in the inventory</returns>
        public bool HasItem(ItemType item)
        {
            return Contents.ContainsKey(item) && Contents[item] != 0;
        }
    }
}