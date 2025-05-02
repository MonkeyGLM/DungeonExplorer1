using System;

namespace DungeonExplorer
{
    public class Potion : Item
    {
        private int healAmount;

        public Potion(string name, int healAmount) : base(name)
        {
            this.healAmount = healAmount;
        }

        public override void Use(Player player)
        {
            player.Heal(healAmount);
            Console.WriteLine($"{player.Name} used {Name} and restored {healAmount} HP!");
        }
    }
}