namespace DungeonExplorer
{
    public static class MonsterOld
    {
        /// <summary>
        /// Makes attacks on player depending on monster type and its abilities
        /// </summary>
        /// <param name="oldPlayer">The player attacked by the monster</param>
        /// <param name="monsterType">The type of monster that is attacking the player</param>
        public static void AttackPlayer(OldPlayer oldPlayer, MonsterType monsterType)
        {
            switch (monsterType)
            {
                case MonsterType.Witch:
                    oldPlayer.GiveBadLuck();
                    break;
                case MonsterType.Thief:
                    oldPlayer.StealItem();
                    oldPlayer.GetAttacked(10);
                    break;
                case MonsterType.Zombie:
                    oldPlayer.GetAttacked(20);
                    break;
            }
        }
    }
}