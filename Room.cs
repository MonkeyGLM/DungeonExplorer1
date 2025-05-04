namespace DungeonExplorer
{
    public class Room
    {
        private string description;
        private Item item;
        public Monster Monster { get; private set; }

        public Room(string description, Item item, Monster monster = null) //initialize room
        {
            this.description = description;
            this.item = item;
            this.Monster = monster;
        }

        public string GetDescription()                                      //returns description
        {
            return description;
        }
        
        public Item Item => item;                                           

        public void PickedUpItem() => item = null;                          //makes the item only able to be picked up once
        
        public void RemoveItem() => item = null;                            //removes item

        public bool HasMonster() => Monster != null && Monster.IsAlive();   //checks if there is a monster

    }
}