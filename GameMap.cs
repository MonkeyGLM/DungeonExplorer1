using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class GameMap
    {
        private List<string> rooms = new List<string>
        {
            "It is a cramped, musty room of moss-covered cobbled walls and a carpeted floor.",
            "It is a damp, narrow hallway covered in cobwebs and moss.",
            "It is a half flooded great hall, with bodies of water between broken pillars and fallen bricks.",
            "It is a chamber, with vines draping like curtains from the cracked beams.",
            "It is a small room overrun with vines, its once-polished floor now broken and overgrown.",
            "It is a wide stone atrium with a collapsed roof, letting roots and rain spill freely inside.",
            "It is a circular vault whose domed ceiling is blanketed in hanging moss and glowing fungus.",
            "It is a vast, domed room split by roots of a colossal tree, its gnarled trunk piercing through stone."
        };

        public string GetRoom()
        {
            Random Rnd = new Random();
            return rooms[Rnd.Next(rooms.Count)];
        }
    }
}