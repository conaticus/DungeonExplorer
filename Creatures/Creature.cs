using System;

namespace DungeonExplorer
{
    public abstract class Creature : IDamageable
    {
        public String Name;
        public int Health;

        protected Creature(String name, int health)
        {
            Name = name;
            Health = health;
        }

        public virtual void TakeDamage(int amount)
        {
            Health -= amount;

            if (Health < 0)
                Health = 0;
        }
    }
}