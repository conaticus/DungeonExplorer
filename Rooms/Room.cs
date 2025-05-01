using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Room
    {
        public String Description;
        public List<Item> Items = new List<Item>();
        public Monster Monster;
        public bool RequiresKey = false;

        public Room North;
        public Room South;
        public Room East;
        public Room West;

        public void RemoveMonster()
        {
            Monster = null;
        }
    }
}