using System;

namespace DungeonExplorer
{
    public class Monster : Creature
    {
        public int AttackDamage;
        
        /// <summary>
        /// Determines whether a monster will flee if their health is low, or if they will continue to fight.
        /// </summary>
        private protected bool _doesFlee;

        public Monster(String name, int health, int attackDamage, bool doesFlee) : base(name, health)
        {
            AttackDamage = attackDamage;
            _doesFlee = doesFlee;
        }

        /// <summary>
        /// When triggered, Monster makes fight or flight decision to flee or stay. If health is <=30%, monster has 1/4 chance of fleeing
        /// </summary>
        /// <returns>Whether or not the monster has flee</returns>
        public bool FightOrFlight()
        {
            if (!_doesFlee)
                return false;
            
            if (Health > MaxHealth * 0.3)
                return false;
            
            // 1/4 chance
            var rnd = new Random();
            var result = rnd.Next(1, 5);
            return result == 1;
        }
    }
}