using System;

namespace DungeonExplorer
{
    public class Player : Creature
    {
        public Inventory Inventory = new Inventory();
        
        public Player(String name) : base(name, 100) { }
    }
}