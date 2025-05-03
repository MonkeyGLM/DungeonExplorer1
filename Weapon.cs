using System;

namespace DungeonExplorer
{
    public class Weapon : Item
    {
        public int Damage { get; private set; }

        public Weapon(string name, int damage) : base(name)
        {
            Damage = damage;
        }

        public override void Use(Player player)
        {
            Console.WriteLine($"{Name} is the weapon of choice use.");
        }
    }
}
