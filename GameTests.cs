using System;
using System.IO;

namespace DungeonExplorer
{
    public static class GameTests
    {
        private static string logPath = "test_log.txt";

        public static void RunAllTests()                                            //runs all the below tests
        {
            using (StreamWriter log = new StreamWriter(logPath, false))
            {
                log.WriteLine(" - Dungeon Explorer Test Log - ");

                TestPlayerHealing(log);
                TestItemPickupAndRemoval(log);
                TestMonsterDamage(log);
                TestPotionUse(log);
                TestWeaponUseOutput(log);

                log.WriteLine(" - Tests Complete - ");
            }

            Console.WriteLine("Tests complete. Results saved to test_log.txt.");    
        }

        private static void TestPlayerHealing(StreamWriter log)                     //tests heal
        {
            var player = new Player("Test Player", 50);
            player.Heal(30);
            string result = player.Health == 80 ? "PASS" : "FAIL";
            log.WriteLine($"TestPlayerHealing: {result} (Expected 80, Got {player.Health})");
        }

        private static void TestItemPickupAndRemoval(StreamWriter log)              //tests item pickup
        {
            var player = new Player("Test Player", 100);
            var sword = new Weapon("Test Sword", 10);
            player.PickUpItem(sword);
            bool hasItem = player.InventoryContents().Contains("Test Sword");
            player.RemoveItem("Test Sword");
            bool removed = !player.InventoryContents().Contains("Test Sword");
            log.WriteLine($"TestItemPickup: {(hasItem ? "PASS" : "FAIL")}");
            log.WriteLine($"TestItemRemoval: {(removed ? "PASS" : "FAIL")}");
        }

        private static void TestMonsterDamage(StreamWriter log)                     //tests damaging monster
        {
            var monster = new Ghoul();
            monster.TakeDamage(5);
            string result = monster.Health == 5 ? "PASS" : "FAIL";
            log.WriteLine($"TestMonsterDamage: {result} (Expected 5, Got {monster.Health})");
        }

        private static void TestPotionUse(StreamWriter log)                         //tests using potion
        {
            var player = new Player("Test Player", 70);
            var potion = new Potion("Healing Potion", 20);
            potion.Use(player);
            string result = player.Health == 90 ? "PASS" : "FAIL";
            log.WriteLine($"TestPotionUse: {result} (Expected 90, Got {player.Health})");
        }

        private static void TestWeaponUseOutput(StreamWriter log)                   //tests using weapon
        {
            var weapon = new Weapon("Debug Sword", 15);
            Console.SetOut(log);
            weapon.Use(new Player("Test Player", 100));
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            log.WriteLine("TestWeaponUseOutput: PASS (Check for correct console output)");
        }
    }
}
