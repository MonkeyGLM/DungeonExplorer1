using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();
        public IEnumerable<Item> Items => items;

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public Item GetItem(string name)
        {
            return items.FirstOrDefault(i => i.Name == name);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
    }
}