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
            return string.Join(", ", inventory.Items.Select(i => i.Name));
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
                item.Use(this);
                inventory.RemoveItem(item);
            }
            else
            {
                Console.WriteLine($"You don't have an item called '{itemName}'.");
            }
        }

        public void Heal(int amount)
        {
            Health += amount;
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
    }
}