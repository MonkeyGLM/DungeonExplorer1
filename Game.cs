using System;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        public Game()
        {
            // Initialize the game with one room and one player
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            player = new Player(name, 100);
            currentRoom = new Room("It is a cramped, musty room of moss-covered cobbled walls and a carpeted floor. There is a small potion on the floor.", "Small Potion");
            
        }
        public void Start()
        {
            // Changed the playing logic into true and populated the while loop
            bool playing = true;

            Console.WriteLine("Welcome", player);
            Console.WriteLine("You enter the dungeon.");

            while (playing)
            {
                // Code your playing logic here
                Console.WriteLine(currentRoom.GetDescription());
                Console.WriteLine("What will you do?");
                Console.WriteLine("- 1. Pick up the item");
                Console.WriteLine("- 2. Check status");
                Console.WriteLine("- 3. Flee like a coward"); 

                string input = Console.ReadLine();
                if (input == "1")
                {
                    if (!string.IsNullOrEmpty(currentRoom.Item))
                    {
                        player.PickUpItem(currentRoom.Item);
                        Console.WriteLine("You picked up a "+ currentRoom.Item);
                        currentRoom.RemoveItem();
                    }
                    else
                    {
                        Console.WriteLine("There is nothing to pick up.");
                    }
                }

                if (input == "2")
                {
                    Console.WriteLine($"Health: {player.Health}");
                    Console.WriteLine("Inventory: " + player.InventoryContents());
                }
                
                if (input == "3")
                {
                    Console.WriteLine("Thanks for playing!");
                    playing = false;
                    break;
                }
            }
        }
    }
}