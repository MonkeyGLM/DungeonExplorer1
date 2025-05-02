using System;

namespace DungeonExplorer
{
    public class Wombat : Monster
    {
        public Wombat() : base("Wombat", 30, 2) {}

        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name} attacks {player.Name} with a flurry of bites!");
            Random Rnd = new Random();
            int RndHit = Rnd.Next(1, 11);
            for (int i = 1; i<=RndHit; i++)
            {
                Console.WriteLine($"{player.Name} was bit for {AttackPower} damage!");
                base.Attack(player);
            }
            Console.WriteLine($"{player.Name} was bit {RndHit} times!");
        }
    }
}