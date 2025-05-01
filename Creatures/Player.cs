using System;

namespace DungeonExplorer
{
    public class Player : Creature
    {
        public int Score = 0;
        
        public Inventory Inventory = new Inventory();
        public Navigation Navigation = new Navigation();
        
        public Player(String name) : base(name, 100) { }

        /// <summary>
        /// Enter a room based on direction from current room, shows error if monster is in current room or room in that direction does not exist. Rooms that require key cannot be entered without key in inventory.
        /// Player is notified if there is a monster in the new room.
        /// </summary>
        /// <param name="direction">Direction to the next room.</param>
        public void EnterRoom(Navigation.Direction direction)
        {
            if (Navigation.CurrentRoom.Monster != null)
            {
                Console.WriteLine("You cannot leave this room until you fight the monster with the 'attack' command.");
                return;
            }

            var room = Navigation.GetRoomAt(direction);
            if (room == null)
            {
                Console.WriteLine("There is no room in that direction.");
                return;
            }
            
            if (room.RequiresKey)
            {
                if (!Inventory.HasItem(new RoomKey()))
                {
                    Console.WriteLine("This room is locked. Find a key to enter it.");
                    return;
                }
                
                Console.WriteLine($"You unlocked the {room.Description} with your key.");
            }
            
            Navigation.EnterRoom(room);

            var monster = Navigation.CurrentRoom.Monster;
            if (monster != null)
                Console.WriteLine($"You encountered a {monster.Name}! Use 'attack' command to attack it.");
        }

        /// <summary>
        /// Removes health from player
        /// </summary>
        /// <param name="amount">Amount of health to remove</param>
        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
            
            Console.WriteLine($"You lost {amount} health. Your health is now {Health}%.");

            if (Health == 0)
            {
                Console.WriteLine("You died, better luck next time.");
                Game.Exit();
            }
        }

        /// <summary>
        /// Adds health to player
        /// </summary>
        /// <param name="amount">Amount of health to add</param>
        public void AddHealth(int amount)
        {
            if (Health + amount > MaxHealth)
                Health = MaxHealth;
            else
                Health += amount;
            
            Console.WriteLine($"Your health is now {Health}%.");
        }

        /// <summary>
        /// Attack the monster that is in the current room using the currently equipped weapon.
        /// </summary>
        public void AttackMonster()
        {
            var monster = Navigation.CurrentRoom.Monster;
            if (monster == null)
            {
                Console.WriteLine("There is no monster in this room to attack.");
                return;
            }
            
            if (Inventory.EquippedWeapon == null)
            {
                Console.WriteLine($"You did not equip a weapon ('equip' command) - the {monster.Name} killed you. Game over.");
                Game.Exit();
                return;
            }
            
            Inventory.EquippedWeapon.Attack(monster);
            Score += Inventory.EquippedWeapon.DamagePerHit;
            
            if (monster.Health > 0)
            {
                Console.WriteLine($"You attacked the {monster.Name}, it is now at {monster.Health} health. Use 'attack' again.");
            }
            else
            {
                Navigation.CurrentRoom.RemoveMonster();

                if (monster is Dragon)
                {
                    Console.WriteLine("You killed the final boss! You have escaped the Dungeon, congratulations!");
                    Console.WriteLine($"Your Score: {Score}");
                    Game.Exit();
                    return;
                }
                
                Console.WriteLine($"You killed the {monster.Name}! You can now progress to the next room.");
                return;
            }

            if (monster.FightOrFlight())
            {
                Console.WriteLine(
                    $"The {monster.Name} decided to flee as its health was too low! You are now safe to progress to another room.");
                Navigation.CurrentRoom.RemoveMonster();
                return;
            }

            var rnd = new Random();
            var value = rnd.Next(1, Inventory.IsShieldEquipped ? 11 : 6);
            
            // 20% chance without shield, 10% chance with shield of taking damage
            if (value == 1)
            {
                TakeDamage(monster.AttackDamage);
                Console.WriteLine($"The {monster.Name} attacked you.");
            }
        }
    }
}