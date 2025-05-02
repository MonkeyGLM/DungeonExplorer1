using System;

namespace DungeonExplorer
{
    public class Ghost : Monster
    {
        public Ghost() : base("Ghost", 10, 10) {}
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name} attacks {player.Name} for {AttackPower} damage!");
            base.Attack(player);
        }
    }
}