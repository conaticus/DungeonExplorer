using System;

namespace DungeonExplorer
{
    public class Weapon : Item
    {
        public int DamagePerHit;

        public Weapon(String name, int damagePerHit) : base(name)
        {
            DamagePerHit = damagePerHit;
        }

        /// <summary>
        /// Attack a specific creature with this weapon and remove from that creature's health
        /// </summary>
        /// <param name="creature">The creature to attack with this weapon</param>
        public void Attack(Creature creature)
        {
            creature.TakeDamage(DamagePerHit);
        }
    }
}