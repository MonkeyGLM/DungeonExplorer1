using System;
using System.Linq;
using System.ComponentModel;
using System.Configuration.Assemblies;
using System.Data;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;
        private GameMap gameMap;

        public Game()
        {
            // Initialize the game with rooms and one player
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            player = new Player(name, 100);
            gameMap = new GameMap();            
        }

        public void Start()
        {
            bool playing = true;

            Item RandomizeItem()                                       //picks a random item
            {
                Random Rndi = new Random();
                int rndItem = Rndi.Next(1, 11);
                if (rndItem <= 3)
                {
                    return null;
                }  
                else if (rndItem <= 7)
                {
                    return new Potion("Small Potion", 30);
                }
                else if (rndItem < 9)
                {
                    return new Weapon("Sword", 10);
                }
                else
                {
                    return new Potion("Big Potion", 60);
                }
            }

            Monster RandomizeMonster()                                  //picks a random monster
            {
                Random RndM = new Random();
                int rndMonster = RndM.Next(1,11);

                if (rndMonster <= 6)
                {
                    return new Ghoul();
                }
                else if (rndMonster == 7)
                {
                    return null;
                }
                else
                {
                    return new Wombat();
                }
            }

            currentRoom = new Room(gameMap.GetRoom(), RandomizeItem(), null);           //makes the first room, guaranteed no monster


            Console.WriteLine($"Welcome, {player.Name}!");
            Console.WriteLine("You enter the dungeon.");
            int equippedBoost = 0;
            int kills = 0;
            int rooms = 0;
            while (playing)                                                             //playing logic
            {
                
                // Code your playing logic here
                Console.WriteLine(currentRoom.GetDescription());                        //writes the description
                int currentTurn = 1;
                while(currentRoom.HasMonster())
                {
                    if (currentTurn == 1)                                               //text when monster appears
                    {
                        Console.WriteLine($"A {currentRoom.Monster.Name} has appeared from the darkness!");
                    }
                    
                    Console.WriteLine($"The {currentRoom.Monster.Name} is on {currentRoom.Monster.Health} HP!");
                    Console.WriteLine($"You are on {player.Health} HP!");
                    Console.WriteLine("What will you do?");
                    Console.WriteLine($"- 1. Fight the {currentRoom.Monster.Name}");
                    Console.WriteLine("- 2. Use an item");
                    Console.WriteLine("- 3. Mercy");

                    string checkinput = Console.ReadLine();                             //checks for input
                    if (checkinput == "1")                                              //attack and counter attack
                    {   
                        int damage = 5 + equippedBoost; 
                        Console.WriteLine($"You attack the {currentRoom.Monster.Name}!");
                        currentRoom.Monster.TakeDamage(damage);
                        Console.WriteLine($"The {currentRoom.Monster.Name} takes {damage} HP of damage!");
                        if (!currentRoom.Monster.IsAlive())
                        {
                            Console.WriteLine($"You defeated the {currentRoom.Monster.Name}!");
                            kills = kills + 1;
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
                    else if (checkinput == "2")                                         //opens the inventory to use an item
                    {
                        var inventoryContents = player.InventoryContents();

                        if (!string.IsNullOrWhiteSpace(inventoryContents))
                        {
                            Console.WriteLine("Inventory: " + player.InventoryContents());
                            Console.WriteLine("Which item would you like to use?");
                            string itemName = Console.ReadLine();
                            var item = player.GetItemByName(itemName);

                            if (item is Weapon inUseWeapon)                             //checks if item is weapon
                            {
                                equippedBoost = 0;
                                Console.WriteLine($"You equip {inUseWeapon.Name} to add {inUseWeapon.Damage} damage to your attack!");
                                equippedBoost = inUseWeapon.Damage;
                                player.UseItem(itemName);
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
                    else if (checkinput == "3")                              //if its a wombat, you can peace option because why not
                    {
                        if (currentRoom.Monster.Name == "Wombat")
                        {
                            Console.WriteLine($"The {currentRoom.Monster.Name} didn't really want to fight anyway!");
                                                    
                            currentRoom.Monster.TakeDamage(10000);
                            if (!currentRoom.Monster.IsAlive())
                            {
                                Console.WriteLine($"The {currentRoom.Monster.Name} ran off!");    
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
                    currentTurn = currentTurn + 1;
                }

                if (playing == true)                                       //if you die to a monster, skips the room search
                {
                    Console.WriteLine("What will you do?");
                    Console.WriteLine("- 1. Search for an item");
                    Console.WriteLine("- 2. Check status");
                    Console.WriteLine("- 3. List items by type");
                    Console.WriteLine("- 4. Move forward to the next room");
                    Console.WriteLine("- 5. End the dungeon crawl");

                    string input = Console.ReadLine();
                    if (input == "1")                                       //search for item
                    {
                        if (currentRoom.Item != null)
                        {
                            player.PickUpItem(currentRoom.Item);
                            Console.WriteLine($"You picked up a {currentRoom.Item.Name}");
                            currentRoom.RemoveItem();
                        }
                        else
                        {
                            Console.WriteLine("There is nothing to pick up.");
                        }
                    }

                    if (input == "2")                                       //checks status
                    {
                        Console.WriteLine($"Health: {player.Health}");
                        Console.WriteLine("Inventory: " + player.InventoryContents());
                    }

                    if (input == "3")                                       //lists item by type, sorted by potency
                    {
                        var weapons = player.GetWeapons();
                        var potions = player.GetPotions();

                        Console.WriteLine("Weapons:");
                        if (weapons.Any())
                        {
                            foreach (var x in weapons.OrderByDescending(x => x.Damage))
                            {
                                Console.WriteLine($"- {x.Name} (Damage: {x.Damage})");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have no weapons.");
                        }

                        Console.WriteLine("\nPotions:");
                        if (potions.Any())
                        {
                            foreach (var y in potions.OrderByDescending(y => y.healAmount2))
                            {
                                Console.WriteLine($"- {y.Name} (Damage: {y.healAmount2})");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have no potions.");
                        }
                    
                    }
                    
                    
                    if (input == "4")                                       //creates the next room and moves on
                    {
                        Console.WriteLine("You move into the next room...");
                        rooms = rooms + 1;
                        currentRoom = new Room(gameMap.GetRoom(), RandomizeItem(), RandomizeMonster());
                    }
                
                    if (input == "5")                                       //quits the game
                    {
                        Console.WriteLine($"You Killed {kills} enemies and explored {rooms} rooms!");
                        Console.WriteLine("Thanks for playing!");
                        playing = false;
                        break;
                    }
                }
            }
        }
    }
}
