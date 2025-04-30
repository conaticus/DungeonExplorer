using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public static class Navigation
    {
        public static Room CurrentRoom;

        public enum Direction
        {
            North,
            East,
            South,
            West
        }
        
        public static void Initialise()
        {
            
            var storage = new Room
            {
                Description = "Storage",
                Items = new List<int>(), // Health Potion
            };
            
            var hallway = new Room
            {
                Description = "Hallway",
                Items = new List<int>(), // Empty
            };
            
            var armoury = new Room
            {
                Description = "Armoury",
                Items = new List<int>(), // Sword, Shield
            };
            
            var chamber = new Room
            {
                Description = "Chamber",
                Monster = null, // Goblin
            };
            
            var cave = new Room
            {
                Description = "Cave",
                Monster = null, // Dragon
            };
            
            var chemist = new Room
            {
                Description = "Chemist",
                Items = new List<int>(), // Health Potion
            };
            
            var lair = new Room
            {
                Description = "Lair",
                Monster = null, // Boss
            };

            // Interconnect rooms based on diagram
            storage.South = hallway;

            hallway.North = storage;
            hallway.South = armoury;
            hallway.East = chamber;

            armoury.North = hallway;
            
            chamber.North = cave;
            chamber.West = hallway;

            cave.East = chemist;
            cave.South = chamber;

            chemist.West = cave;
            chemist.South = lair;

            lair.North = chemist;

            CurrentRoom = storage; // Root of the Room Map
        }

        public static void DisplayEnteredMessage()
        {
            Console.WriteLine($"You entered '{CurrentRoom.Description}'.");
        }

        public static void EnterRoom(Direction direction)
        {
            Room targetRoom = null;
            
            switch (direction)
            {
                case Direction.North:
                    targetRoom = CurrentRoom.North;
                    break;
                case Direction.East:
                    targetRoom = CurrentRoom.East;
                    break;
                case Direction.South:
                    targetRoom = CurrentRoom.South;
                    break;
                case Direction.West:
                    targetRoom = CurrentRoom.West;
                    break;
            }

            if (targetRoom == null)
            {
                Console.WriteLine("There is no room in that direction.");
                return;
            }
            
            CurrentRoom = targetRoom;
            DisplayEnteredMessage();
        }
    }
}