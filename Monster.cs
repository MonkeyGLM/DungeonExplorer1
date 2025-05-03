using System;

namespace DungeonExplorer
{
    public class Monster : Creature, IDamageable
    {
        public int AttackPower { get; protected set; }

        public Monster(string name, int health, int attackPower) : base(name, health)
        {
            AttackPower = attackPower;
        }

        public override void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }

        public override bool IsAlive()
        {
            return Health > 0;
        }

        public virtual void Attack(Player player)
        {
            player.TakeDamage(AttackPower);
        }
    }
}