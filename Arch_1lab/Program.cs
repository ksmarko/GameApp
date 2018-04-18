using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Arch_1lab
{
    class Program
    {
        static Settings settings;
        static Game game;

        static void Main(string[] args)
        {
            MainMenu();
            Console.ReadKey();
        }
        
        public static void MainMenu()
        {
            WriteMessage(ConsoleColor.Yellow, "\nMenu\n");
            Console.WriteLine("\n1 - Set parameters\n2 - Select game\n3 - Get rules\n0 - Exit");

            int input = 0;

            if (int.TryParse(Console.ReadLine(), out input))
                switch (input)
                {
                    case 1: SetParameters(); break;
                    case 2: SelectGame(); break;
                    case 3: GetRules(); break;
                    case 0: Environment.Exit(0); break;
                    default: WriteMessage(ConsoleColor.Red, "Invalid key"); MainMenu(); break;
                }
            else
            {
                WriteMessage(ConsoleColor.Red, "Invalid key");
                MainMenu();
            }
        }

        public static void GetRules()
        {
            WriteMessage(ConsoleColor.Yellow, "\nRules\n");
            Console.WriteLine(SelectToGame().GetRules());
            Console.ReadKey();
            GetRules();
        }

        public static void SelectGame()
        {
            WriteMessage(ConsoleColor.Yellow, "\nSelect game menu\n");
            SelectToGame();
            VerifyGame();
        }

        public static void VerifyGame()
        {
            if (settings == null)
            {
                WriteMessage(ConsoleColor.Red, "Please set parameters");
                MainMenu();
            }

            if (game.Verify(settings))
                PlayGame();
            else
            {
                WriteMessage(ConsoleColor.Red, "Please set necessary settings for this game or select another\n");
                MainMenu();
            }
        }

        public static void PlayGame()
        {
            WriteMessage(ConsoleColor.Yellow, "\nPlease read the rules\n");
            Console.WriteLine(game.GetRules());
            Console.ReadKey();
            WriteMessage(ConsoleColor.Yellow, "\nLet's start!\n");
            game.GameEvent += new Game.GameStateHandler((string message) => Console.WriteLine(message));
            game.Play();
            Console.ReadKey();
            MainMenu();
        }

        public static void SetParameters()
        {
            WriteMessage(ConsoleColor.Yellow, "\nSettings\n");

            int minPlayersCount;
            bool chips;
            bool gameField;
            bool cards;
            bool cube;
            bool money;

            Console.WriteLine("Playsers count: ");
            minPlayersCount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Do you have a chips? 0 - No, 1 - Yes");
            chips = Convert.ToInt32(Console.ReadLine()) == 0 ? false : true;

            Console.WriteLine("Do you have a game field? 0 - No, 1 - Yes");
            gameField = Convert.ToInt32(Console.ReadLine()) == 0 ? false : true;

            Console.WriteLine("Do you have a cards? 0 - No, 1 - Yes");
            cards = Convert.ToInt32(Console.ReadLine()) == 0 ? false : true;

            Console.WriteLine("Do you have a cube? 0 - No, 1 - Yes");
            cube = Convert.ToInt32(Console.ReadLine()) == 0 ? false : true;

            Console.WriteLine("Do you have a game money? 0 - No, 1 - Yes");
            money = Convert.ToInt32(Console.ReadLine()) == 0 ? false : true;

            WriteMessage(ConsoleColor.Magenta, "\nIf you want to add this parameters for the game, please write it number, else - 0");
            settings = new Settings(minPlayersCount, chips, gameField, cards, cube, money);

            SelectToGame().Settings = settings;

            WriteMessage(ConsoleColor.Magenta, $"Assigned to {game.GetType().Name}");
            MainMenu();
        }

        private static Game SelectToGame()
        {
            Console.WriteLine("Select game: \n1 - Mafia\n2 - Monopolia\n3 - Alias\n0 - Back");

            string line = Console.ReadLine();
            int index = 0;

            if (int.TryParse(line, out index))
            {
                switch (index)
                {
                    case 1: game = new Mafia(settings.PlayersCount); break;
                    case 2: game = new Monopolia(settings.PlayersCount); break;
                    case 3: game = new Alias(settings.PlayersCount); break;
                    case 0: MainMenu(); break;
                    default: WriteMessage(ConsoleColor.Red, "Invalid key"); break;
                }
                return game;
            }
            else
            {
                WriteMessage(ConsoleColor.Red, "Invalid key");
                SelectToGame();
                return null;
            }                
        }
        
        private static void WriteMessage(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
