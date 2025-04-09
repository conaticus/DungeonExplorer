using System;

namespace DungeonExplorer
{
    public class Monster : Creature
    {
        /// <summary>
        /// Determines whether a monster will flee if their health is low, or if they will continue to fight.
        /// </summary>
        private protected bool _doesFlee;

        public Monster(String name, int health, bool doesFlee) : base(name, health)
        {
            _doesFlee = doesFlee;
        }

        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);

            if (!_doesFlee)
                return;
            
            // If health is <30%, 50% chance of fleeing? - simpler way of doing this without random? Where does it go if it flees?
            throw new NotImplementedException();
        }
    }
}