using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public abstract class Creature                              //base class for creatures
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }

        public Creature(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public abstract void TakeDamage(int amount);
        public abstract bool IsAlive();
    }
}