namespace DungeonExplorer
{

    public interface ICollectible
    {
        void Use(Player player);
    }
    public abstract class Item : ICollectible
    {
        public string Name { get; protected set; }

        public Item(string name)
        {
            Name = name;
        }

        public abstract void Use(Player player);
    }
}