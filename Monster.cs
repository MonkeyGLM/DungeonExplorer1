using System;

namespace DungeonExplorer
{
    public class Monster : Creature, IDamageable                //base class for monster    
    {
        public int AttackPower { get; protected set; }

        public Monster(string name, int health, int attackPower) : base(name, health)
        {
            AttackPower = attackPower;
        }

        public override void TakeDamage(int amount)             //to take damage
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }

        public override bool IsAlive()                          //to check if its alive
        {
            return Health > 0;
        }

        public virtual void Attack(Player player)               //to attack a player
        {
            player.TakeDamage(AttackPower);
        }
    }
}