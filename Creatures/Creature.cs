using System;

namespace DungeonExplorer
{
    public abstract class Creature : IDamageable, IEntity
    {
        public int MaxHealth;
        public int Health;
        
        public string Name { get; set; }

        protected Creature(String name, int health)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
        }

        /// <summary>
        /// Removes specific amount from health, health will not go below zero.
        /// </summary>
        /// <param name="amount">Amount of health to take from creature.</param>
        public virtual void TakeDamage(int amount)
        {
            Health -= amount;

            if (Health < 0)
                Health = 0;
        }
    }
}