using System;

namespace DungeonExplorer
{
    public class Ghoul : Monster                        //inherits monster
    {
        public Ghoul() : base("Ghoul", 10, 15) {}       //creates a ghoul
        public override void Attack(Player player)      //ghoul's attack
        {
            Console.WriteLine($"{Name} attacks {player.Name} for {AttackPower} damage!");
            base.Attack(player);
        }
    }
}