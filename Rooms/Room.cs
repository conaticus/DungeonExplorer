using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Room
    {
        public String Description;
        public List<int> Items = new List<int>();
        public Monster Monster;

        public Room North;
        public Room South;
        public Room East;
        public Room West;
    }
}