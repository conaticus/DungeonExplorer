using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    class Game
    {
        private OldPlayer _oldPlayer;
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
            new Command("next", "Moves player to the next room"),
            new Command("use", "Use inventory item if player has it in their inventory"),
            new Command("inventory", "Displays items in inventory"),
            new Command("health", "Displays player's health"),
            new Command("help", "A list of all available commands"),
            new Command("exit", "Quits the game")
        };
        
        /// <summary>
        /// Starts the Game Flow
        /// </summary>
        public void Start()
        {
            Navigation.Initialise();
            
            Console.WriteLine("Welcome to the Dungeon Game.");
            string playerName = ReadInput("Please enter your name");
            _oldPlayer = new OldPlayer(playerName);
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
                        Navigation.EnterRoom(Navigation.Direction.North);
                        break;
                    case "down":
                        Navigation.EnterRoom(Navigation.Direction.South);
                        break;
                    case "right":
                        Navigation.EnterRoom(Navigation.Direction.East);
                        break;
                    case "left":
                        Navigation.EnterRoom(Navigation.Direction.West);
                        break;
                    
                    case "use":
                        UseItem();
                        break;
                    case "inventory":
                        DisplayInventory();
                        break;
                    case "health":
                        DisplayHealth();
                        break;

                    case "help":
                        Help();
                        break;
                    case "quit":
                        Quit();
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
        /// Use item if that item exists in the player's inventory. Multiple choice dialogue is triggered to select an item type.
        /// </summary>
        private void UseItem()
        {
            string[] itemTypes = Enum.GetNames(typeof(ItemType));
            ItemType itemChoice = (ItemType)ReadMultiChoiceInt("Please choose an item to use", itemTypes);
            
            if (!_oldPlayer.HasItem(itemChoice))
            {
                Console.WriteLine($"You do not have a {itemChoice} in your inventory.");
                return;
            }
            
            _oldPlayer.UseItem(itemChoice);
        }

        /// <summary>
        /// Displays all items in the player's inventory as well as the number of items in there.s
        /// </summary>
        private void DisplayInventory()
        {
            Console.WriteLine($"Inventory: {_oldPlayer.InventoryContents()}");
        }
        
        /// <summary>
        /// Displays player's health
        /// </summary>
        private void DisplayHealth()
        {
            Console.WriteLine($"Health: {_oldPlayer.Health}%");
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
        public static void Quit()
        {
            _playing = false;
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}