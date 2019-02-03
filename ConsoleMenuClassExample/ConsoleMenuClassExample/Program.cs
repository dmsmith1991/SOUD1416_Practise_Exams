using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleMenuClassExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Let's create a new ConsoleMenu() instance
            ConsoleMenu pet_menu = new ConsoleMenu();
            // Set a custom prompt message
            pet_menu.PromptText = "Out of the following options, which is your favourite pet?";
            // Now, let's add our menu options
            // ConsoleMenu -> AddOptions() allows us to submit all our options in a single array!
            // new string[] {} creates a temporary string array just for this function call. It's automatically destroyed before we move to the next line (the class instance stores all the values we give it, so there's no need to keep a duplicate list around)
            pet_menu.AddOptions(new string[] { "Cat", "Dog", "Fish", "Hamster", "Rabbit", "Snake" });

            // Let's create a second entirely seperate menu (we're creating a new instance of ConsoleMenu, with it's own independant state)
            ConsoleMenu meal_menu = new ConsoleMenu();
            meal_menu.PromptText = "Out of the following options, which is your favourite meal?";
            meal_menu.AddOptions(new string[] { "Breakfast", "Lunch", "Dinner" });

            // Now that we've finished building our menus, lets use them!
            // Pet menu first
            Console.WriteLine("\n[Main] Running the pet menu...");
            string chosen_pet = pet_menu.Run(); // Call Run() method, which returns selected option
            if(chosen_pet != null)              // A return value of null means user opted to skip (see ConsoleMenu -> Run())
            {
                Console.WriteLine("\n[Main] Your favourite pet is: " + chosen_pet);
            }
            else
            {
                Console.WriteLine("\n[Main] User didn't specify a favourite pet");
            }

            // Now the meal menu
            Console.WriteLine("\n[Main] Running the meal menu...");
            string chosen_meal = meal_menu.Run();
            if (chosen_meal != null)
            {
                Console.WriteLine("\n[Main] Your favourite meal is: " + chosen_meal);
            }
            else
            {
                Console.WriteLine("\n[Main] User didn't specify a favourite meal");
            }

            // Bonus menu (sorry Ryan :))
            BonusQuestionBanner();
            // Urghhh... I'm just going to ommit the comments. I'm sure you all get the idea by now
            ConsoleMenu ryan_menu = new ConsoleMenu();
            ryan_menu.PromptText = "Why does our classmate Ryan W. have the nickname 'Charlie'?";
            ryan_menu.AddOption("His favourite comedian is Charlie Chaplin");
            ryan_menu.AddOption("His dog named Charlie woke him up one morning by licking his private parts");
            ryan_menu.AddOption("His favourite YouTube video is 'Charlie Bit My Finger!'");
            string answer = ryan_menu.Run();
            if(answer.Contains("private parts"))
            {
                Console.WriteLine("\n[Main] That is the correct answer. Who'd have thought it!");
            }
            else
            {
                Console.WriteLine("\n[Main] Wrong answer, better luck next time.");
            }

            // See how useful classes can be? :)
            // The ConsoleMenu() class can be easily reused in other projects, by just adding the ConsoleMenu.cs file to the solution!
            // Feel free to review ConsoleMenu()'s contents to see how this all works

            Console.WriteLine("\nProgram finished. Press enter to exit...");
            Console.ReadLine();
        }

        static void BonusQuestionBanner()
        {
            /*
            Console.WriteLine(@"<-.(`-')            <-. (`-')_             (`-').->     <-.(`-')              (`-')  _ (`-').->(`-')      _                <-. (`-')_ ");
            Console.WriteLine(@" __( OO)      .->      \( OO) )     .->    ( OO)_        __( OO)       .->    ( OO).-/ ( OO)_  ( OO).->  (_)         .->      \( OO) )");
            Console.WriteLine(@"'-'---.\ (`-')----. ,--./ ,--/ ,--.(,--.  (_)--\_)      '-'---\_) ,--.(,--.  (,------.(_)--\_) /    '._  ,-(`-')(`-')----. ,--./ ,--/ ");
            Console.WriteLine(@"| .-. (/ ( OO).-.  '|   \ |  | |  | |(`-')/    _ /     |  .-.  |  |  | |(`-') |  .---'/    _ / |'--...__)| ( OO)( OO).-.  '|   \ |  | ");
            Console.WriteLine(@"| '-' `.)( _) | |  ||  . '|  |)|  | |(OO )\_..`--.     |  | | <-' |  | |(OO )(|  '--. \_..`--. `--.  .--'|  |  )( _) | |  ||  . '|  |)");
            Console.WriteLine(@"| /`'.  | \|  |)|  ||  |\    | |  | | |  \.-._)   \    |  | |  |  |  | | |  \ |  .--' .-._)   \   |  |  (|  |_/  \|  |)|  ||  |\    | ");
            Console.WriteLine(@"| '--'  /  '  '-'  '|  | \   | \  '-'(_ .'\       /    '  '-'  '-.\  '-'(_ .' |  `---.\       /   |  |   |  |'->  '  '-'  '|  | \   | ");
            Console.WriteLine(@"`------'    `-----' `--'  `--'  `-----'    `-----'      `-----'--' `-----'    `------' `-----'    `--'   `--'      `-----' `--'  `--' ");
            */

            Console.WriteLine("");
            Console.WriteLine(@" ________  ________  ________   ___  ___  ________           ________  ________  ___  ___  ________   ________     ");
            Console.WriteLine(@"|\   __  \|\   __  \|\   ___  \|\  \|\  \|\   ____\         |\   __  \|\   __  \|\  \|\  \|\   ___  \|\   ___ \    ");
            Console.WriteLine(@"\ \  \|\ /\ \  \|\  \ \  \\ \  \ \  \\\  \ \  \___|_        \ \  \|\  \ \  \|\  \ \  \\\  \ \  \\ \  \ \  \_|\ \   ");
            Console.WriteLine(@" \ \   __  \ \  \\\  \ \  \\ \  \ \  \\\  \ \_____  \        \ \   _  _\ \  \\\  \ \  \\\  \ \  \\ \  \ \  \ \\ \  ");
            Console.WriteLine(@"  \ \  \|\  \ \  \\\  \ \  \\ \  \ \  \\\  \|____|\  \        \ \  \\  \\ \  \\\  \ \  \\\  \ \  \\ \  \ \  \_\\ \ ");
            Console.WriteLine(@"   \ \_______\ \_______\ \__\\ \__\ \_______\____\_\  \        \ \__\\ _\\ \_______\ \_______\ \__\\ \__\ \_______\");
            Console.WriteLine(@"    \|_______|\|_______|\|__| \|__|\|_______|\_________\        \|__|\|__|\|_______|\|_______|\|__| \|__|\|_______|");
            Console.WriteLine(@"                                            \|_________|                                                           ");
            Console.WriteLine("");
        }
    }
}
