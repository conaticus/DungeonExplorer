using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Item : IEntity
    {
        public string Name { get; set; }
        public bool IsEquippable;

        public Item(String name, bool isEquippable = true)
        {
            Name = name;
            IsEquippable = isEquippable;
        }
    }
}