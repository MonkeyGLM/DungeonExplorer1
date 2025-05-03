using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System;
using System.CodeDom;

namespace DungeonExplorer
{
    public class Player : Creature, IDamageable
    {
        private Inventory inventory;

        public Player(string name, int health) : base(name, health) 
        {
            inventory = new Inventory();
        }
        public void PickUpItem(Item item)
        {
            inventory.AddItem(item);
        }
        public string InventoryContents()
        {
            return string.Join(", ", inventory.Items.Select(i =>
            {
                if (i is Weapon w)
                {
                    return $"{w.Name} (Damage: +{w.Damage})";
                }
                if (i is Potion p)
                {
                    return $"{p.Name} (Heals {p.healAmount2} HP)";
                }
                return i.Name;
            }));
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

        public void UseItem(string itemName)
        {
            var item = inventory.GetItem(itemName);
            if (item != null)
            {
                if (item is Potion)
                {
                    if (Health != 100)
                    {
                        item.Use(this);
                        inventory.RemoveItem(item);
                    }
                    else
                    {
                        Console.WriteLine("You are already on full health!");
                    }
                }
                else
                {
                    inventory.RemoveItem(item);
                }
            }
            else
            {
                Console.WriteLine($"You don't have an item called '{itemName}'.");
            }
        }

        public void Heal(int amount)
        {
            Health += amount;
            if (Health > 100)
            {
                Health = 100;
            }
            Console.WriteLine($"{Name} healed for {amount}. Current Health: {Health}");
        }

        public Item GetItemByName(string name)
        {
            return inventory.GetItem(name);
        }

        public void RemoveItem(string name)
        {
            var item = inventory.GetItem(name);
            if (item != null)
            {
                inventory.RemoveItem(item);
            }
        }

        public IEnumerable<Weapon> GetWeapons()
        {   
            return inventory.Items.OfType<Weapon>();
        }

        public IEnumerable<Potion> GetPotions()
        {
            return inventory.Items.OfType<Potion>();
        }
    }
}