using System;

namespace DungeonExplorer
{
    public class Potion : Item
    {
        public int healAmount2 {get; private set; }

        public Potion(string name, int healAmount) : base(name)
        {
            healAmount2 = healAmount;
        }

        public override void Use(Player player)
        {
            player.Heal(healAmount2);
            Console.WriteLine($"{player.Name} used {Name} and restored {healAmount2} HP!");
        }
    }
}