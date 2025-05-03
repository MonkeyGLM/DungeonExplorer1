using System;

namespace DungeonExplorer
{
    public class Ghoul : Monster
    {
        public Ghoul() : base("Ghoul", 10, 15) {}
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name} attacks {player.Name} for {AttackPower} damage!");
            base.Attack(player);
        }
    }
}