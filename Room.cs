namespace DungeonExplorer
{
    public class Room
    {
        private string description;
        private Item item;
        public Monster Monster { get; private set; }

        public Room(string description, Item item, Monster monster = null)
        {
            this.description = description;
            this.item = item;
            this.Monster = monster;
        }

        public string GetDescription()
        {
            return description;
        }
        
        public Item Item => item;

        public void PickedUpItem() => item = null;
        
        public void RemoveItem() => item = null;

        public bool HasMonster() => Monster != null && Monster.IsAlive();

    }
}