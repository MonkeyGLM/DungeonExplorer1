using System;
using System.Data;
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
            Item newPotion = new Potion("Small Potion", 20);
            Item sword = new Weapon("Sword", 10);
            currentRoom = new Room("It is a cramped, musty room of moss-covered cobbled walls and a carpeted floor. There is a small potion on the floor. You hear a wombat hiss from the dark corner of the room.", newPotion, new Rat());
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
                
                if (currentRoom.HasMonster())
                {
                    Console.WriteLine($"A {currentRoom.Monster.Name} is here!");
                    Console.WriteLine("What will you do?");
                    Console.WriteLine($"- 1. Fight the {currentRoom.Monster.Name}");
                    Console.WriteLine("- 2. Use an item");
                    Console.WriteLine("- 3. Beg for mercy");

                    int equippedBoost = 0;

                    string checkinput = Console.ReadLine();
                    if (checkinput == "1")
                    {   
                        int damage = 5 + equippedBoost; 
                        Console.WriteLine($"You attack the {currentRoom.Monster.Name}!");
                        currentRoom.Monster.TakeDamage(damage);
                        if (!currentRoom.Monster.IsAlive())
                        {
                            Console.WriteLine($"You defeated the {currentRoom.Monster.Name}!");
                        }
                        else
                        {
                            currentRoom.Monster.Attack(player);
                            if (!player.IsAlive())
                            {
                                Console.WriteLine("You have been defeated!");
                                playing = false;
                                break;
                            }
                        }
                    }
                    else if (checkinput == "2")
                    {
                        var inventoryContents = player.InventoryContents();

                        if (!string.IsNullOrWhiteSpace(inventoryContents))
                        {
                            Console.WriteLine("Inventory: " + player.InventoryContents());
                            Console.WriteLine("Which item would you like to use?");
                            string itemName = Console.ReadLine();
                            var item = player.GetItemByName(itemName);

                            if (item is Weapon inUseWeapon)
                            {
                                Console.WriteLine($"You equip {inUseWeapon.Name}!");
                                equippedBoost = inUseWeapon.Damage;
                                
                            }
                            else
                            {
                                player.UseItem(itemName);
                            }
                        }   
                        else
                        {
                            Console.WriteLine("You have no items to use!");
                        }

                        currentRoom.Monster.Attack(player);
                        if (!player.IsAlive())
                        {
                            Console.WriteLine("You have been defeated!");
                            playing = false;
                            break;
                        }
                    }
                    else if (checkinput == "3")
                    {
                        if (currentRoom.Monster.Name == "Wombat")
                        {
                            Console.WriteLine($"The {currentRoom.Monster.Name} didn't really want to fight anyway!");
                        
                            currentRoom.Monster.TakeDamage(10000);
                            if (!currentRoom.Monster.IsAlive())
                            {
                                Console.WriteLine($"You defeated the {currentRoom.Monster.Name}!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You wasted your turn dummy.");

                            currentRoom.Monster.Attack(player);
                            if (!player.IsAlive())
                            {
                                Console.WriteLine("You have been defeated!");
                                playing = false;
                                break;
                            }
                        }
                    }
                    
                }
                Console.WriteLine("What will you do?");
                Console.WriteLine("- 1. Pick up the item");
                Console.WriteLine("- 2. Check status");
                Console.WriteLine("- 3. End the dungeon crawl");

                string input = Console.ReadLine();
                if (input == "1")
                {
                    if (currentRoom.Item != null)
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
