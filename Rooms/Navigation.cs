using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Navigation
    {
        public Room CurrentRoom;

        public enum Direction
        {
            North,
            East,
            South,
            West
        }

        public Navigation()
        {
            var storage = new Room
            {
                Description = "Storage",
            };
            
            var hallway = new Room
            {
                Description = "Hallway",
                Items = { new Sword() },
            };

            var arena = new Room
            {
                Description = "Arena",
                Monster = new Zombie(),
            };
            
            var armoury = new Room
            {
                Description = "Armoury",
                Items = { new Crossbow(), new Shield(), new RoomKey() },
            };
            
            var chamber = new Room
            {
                Description = "Chamber",
                Monster = new Goblin(),
            };
            
            var cave = new Room
            {
                Description = "Cave",
                Monster = new Zombie(),
            };
            
            var chemist = new Room
            {
                Description = "Chemist",
                Items = { new HealthPotion(), new HealthPotion() },
            };
            
            var lair = new Room
            {
                Description = "Lair",
                Monster = new Dragon(),
                RequiresKey = true,
            };

            // Interconnect rooms based on diagram
            storage.South = hallway;

            hallway.North = storage;
            hallway.East = chamber;
            hallway.South = arena;

            arena.North = hallway;
            arena.East = armoury;

            armoury.West = arena;
            
            chamber.North = cave;
            chamber.West = hallway;

            cave.East = chemist;
            cave.South = chamber;

            chemist.West = cave;
            chemist.South = lair;

            lair.North = chemist;

            CurrentRoom = storage; // Root of the Room Map
        }

        /// <summary>
        /// Gets room in specific direction based on the current room, returns null if no room is in that direction
        /// </summary>
        /// <param name="direction">Direction of the next room</param>
        /// <returns>The room in that specific direction (if exists)</returns>
        public Room GetRoomAt(Direction direction)
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

            return targetRoom;
        }
        
        /// <summary>
        /// Enter a specific room
        /// </summary>
        /// <param name="room">The room to enter</param>
        public void EnterRoom(Room room)
        {
            CurrentRoom = room;
            Console.WriteLine($"You entered '{room.Description}'.");
        }

        /// <summary>
        /// Enter room in direction based on current room
        /// </summary>
        /// <param name="direction">Direction of the room to enter</param>
        public void EnterRoom(Direction direction)
        {
            var room = GetRoomAt(direction);
            if (room == null)
            {
                Console.WriteLine("There is no room in that direction.");
                return;
            }

            EnterRoom(room);
        }
    }
}