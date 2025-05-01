using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    class Game
    {
        private Player _player;
        private static bool _playing = true;

        private struct Command
        {
            public String Name;
            public String Description;

            public Command(String name, String description)
            {
                Name = name;
                Description = description;
            }
        }

        private List<Command> commands = new List<Command>()
        {
            new Command("up", "Move to the room above"),
            new Command("down", "Move to the room below"),
            new Command("left", "Move to the room on the left"),
            new Command("right", "Move to the room on the right"),
            new Command("search", "Searches room and adds any found items to player's inventory"),
            new Command("equip", "Equip/Use an item in player's inventory"),
            new Command("attack", "Attacks the monster in the current room using the player's equipped weapon"),
            new Command("inventory", "Displays items in player's inventory"),
            new Command("weaponstrengths", "Displays all weapons in player's inventory and orders them by strength in descending order"),
            new Command("health", "Displays player's health"),
            new Command("help", "A list of all available commands"),
            new Command("exit", "Quits the game")
        };
        
        /// <summary>
        /// Starts the Game Flow
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Welcome to the Dungeon Game.");
            string playerName = ReadInput("Please enter your name");
            
            _player = new Player(playerName);
            
            Console.WriteLine($"Thanks, {playerName}.");
            Console.WriteLine();
            
            while (_playing)
                ProcessCommand();
        }

        /// <summary>
        /// Prompts & Processes command entered by the player
        /// </summary>
        private void ProcessCommand()
        {
            bool processed = true;

            do
            {
                string commandName = ReadInput("Enter a command, or type 'help' for a list of commands");
                switch (commandName.Trim().ToLower())
                {
                    case "up":
                        _player.EnterRoom(Navigation.Direction.North);
                        break;
                    case "down":
                        _player.EnterRoom(Navigation.Direction.South);
                        break;
                    case "right":
                        _player.EnterRoom(Navigation.Direction.East);
                        break;
                    case "left":
                        _player.EnterRoom(Navigation.Direction.West);
                        break;
                    
                    case "attack":
                        _player.AttackMonster();
                        break;
                    
                    case "search":
                        SearchRoom();
                        break;
                    
                    case "equip":
                        EquipItem();
                        break;
                    
                    case "inventory":
                        DisplayInventory();
                        break;
                    case "weaponstrengths":
                        DisplayWeaponsByStrength();
                        break;
                    case "health":
                        DisplayHealth();
                        break;

                    case "help":
                        Help();
                        break;
                    
                    case "exit":
                        Exit();
                        break;
                    default:
                        processed = false;
                        Console.WriteLine($"'{commandName}' is not a valid command.");
                        break;
                }
            } while (!processed);
            
            Console.WriteLine();
        }
 
        /// <summary>
        /// Displays all items in the player's inventory as well as the number of items in there.s
        /// </summary>
        private void DisplayInventory()
        {
            Console.WriteLine($"Inventory: {_player.Inventory.InventoryContents()}");
        }
        
        /// <summary>
        /// Sorts all player's Weapons by strength in descending order & displays them
        /// </summary>
        private void DisplayWeaponsByStrength()
        {
            var weapons = _player.Inventory.Contents.Values
                .Select(i => i.Item1)
                .OfType<Weapon>()
                .ToList();

            if (weapons.Count == 0)
            {
                Console.WriteLine("You do not have any weapons in your inventory.");
                return;
            }
            
            var sortedWeapons = weapons
                .OrderByDescending(w => w.DamagePerHit)
                .Select(w => $"{w.Name}: {w.DamagePerHit} Damage per Hit");

            Console.WriteLine(String.Join("\n", sortedWeapons));
        }
        
        /// <summary>
        /// Displays player's health
        /// </summary>
        private void DisplayHealth()
        {
            Console.WriteLine($"Health: {_player.Health}%");
        }

        /// <summary>
        /// Displays list of commands
        /// </summary>
        private void Help()
        {
            foreach (Command command in commands)
                Console.WriteLine($"{command.Name}: {command.Description}");
        }

        /// <summary>
        /// Reads text input from the player. Repeats prompt until non-empty string is entered.
        /// </summary>
        /// <param name="message">Message that is displayed when player is prompted.</param>
        /// <returns>Input entered by player.</returns>
        private string ReadInput(string message)
        {
            string input = null;

            while (String.IsNullOrEmpty(input))
            {
                Console.Write($"{message}: ");
                input = Console.ReadLine();
            }

            return input;
        }

        /// <summary>
        /// Equip an item in the player's inventory, if it is equippable
        /// </summary>
        public void EquipItem()
        {
            var inventoryItems = _player.Inventory.Contents
                .Where(i => i.Value.Item2 != 0 && i.Value.Item1.IsEquippable)
                .Select(i => i.Key)
                .ToArray();

            if (inventoryItems.Length == 0)
            {
                Console.WriteLine("You do not have any items in your inventory. Use 'search' command on rooms to find them.");
                return;
            }
            
            var choice = ReadMultiChoiceInt("Choose an item to equip", inventoryItems);
            var itemName = inventoryItems[choice];
            
            var item = _player.Inventory.GetItemByName(itemName);
            
            if (item is Potion potion)
            {
                potion.ApplyTo(_player);
                _player.Inventory.UseItem(itemName);
                return;
            }

            if (item is Weapon weapon)
            {
                _player.Inventory.EquippedWeapon = weapon;
                Console.WriteLine($"Your equipped weapon is now a '{weapon.Name}'");
                return;
            }
            
            if (item is Shield)
            {
                _player.Inventory.IsShieldEquipped = true;
                Console.WriteLine("You now have a shield equipped. This will make you less likely to take damage when attacked.");
                return;
            }
        }

        /// <summary>
        /// Search a room for any items, if any exist, add them to player's inventory
        /// </summary>
        public void SearchRoom()
        {
            var currentRoom = _player.Navigation.CurrentRoom;
            if (currentRoom.Items.Count == 0)
            {
                Console.WriteLine("No items were found in this room.");
                return;
            }

            foreach (var item in currentRoom.Items)
            {
                _player.Inventory.PickupItem(item);
                Console.WriteLine($"You Found 1x '{item.Name}' (use 'equip' to use it)");
            }
            
            currentRoom.Items.Clear();
        }

        /// <summary>
        /// Prompts multiple choice input based on array provided.
        /// </summary>
        /// <param name="message">Message that displays when prompting player for choice input</param>
        /// <param name="choices">List of choices that are displayed numerically with respective strings</param>
        /// <returns>Returns index of array player chose</returns>
        private int ReadMultiChoiceInt(String message, string[] choices)
        {
            string choiceDisplay = "";
            for (var i = 0; i < choices.Length; i++)
            {
                string choice = choices[i];
                choiceDisplay += $"{i + 1}: {choice}\n";
            }

            int numberChoice = -1;
            while (true)
            {
                Console.WriteLine(choiceDisplay);
                Console.Write($"{message}: ");
                
                string inputRaw = Console.ReadLine();
                if (!Int32.TryParse(inputRaw, out numberChoice))
                {
                    Console.WriteLine("Please enter a valid number");
                    continue;
                }
                
                if (numberChoice > choices.Length)
                {
                    Console.WriteLine("That is not a valid choice.");
                    continue;
                }
                
                break;
            }

            return numberChoice - 1;
        }
        
        /// <summary>
        /// Quits the game and waits for keyboard input to exit process
        /// </summary>
        public static void Exit()
        {
            _playing = false;
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}