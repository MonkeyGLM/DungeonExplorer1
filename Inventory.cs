using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();
        public IEnumerable<Item> Items => items;

        public void AddItem(Item item)                          //adds an item
        {
            items.Add(item);
        }

        public Item GetItem(string name)                        //gets an item
        {
            return items.FirstOrDefault(i => i.Name == name);
        }

        public void RemoveItem(Item item)                       //removes an item
        {
            items.Remove(item);
        }
    }
}