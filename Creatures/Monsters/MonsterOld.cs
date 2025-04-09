namespace DungeonExplorer
{
    public static class MonsterOld
    {
        /// <summary>
        /// Makes attacks on player depending on monster type and its abilities
        /// </summary>
        /// <param name="player">The player attacked by the monster</param>
        /// <param name="monsterType">The type of monster that is attacking the player</param>
        public static void AttackPlayer(Player player, MonsterType monsterType)
        {
            switch (monsterType)
            {
                case MonsterType.Witch:
                    player.GiveBadLuck();
                    break;
                case MonsterType.Thief:
                    player.StealItem();
                    player.GetAttacked(10);
                    break;
                case MonsterType.Zombie:
                    player.GetAttacked(20);
                    break;
            }
        }
    }
}