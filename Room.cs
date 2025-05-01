namespace DungeonExplorer
{
    public class Room
    {
        private string description;
        private string item;

        public Room(string description, string item)
        {
            this.description = description;
            this.item = item;
        }

        public string GetDescription()
        {
            return description;
        }
        public string Item 
        {
            get { return item; } 
        }

        public void PickedUpItem()
        {
            item = null;
        }
        
        public void RemoveItem()
        {
            item = null;
        }
    }
}