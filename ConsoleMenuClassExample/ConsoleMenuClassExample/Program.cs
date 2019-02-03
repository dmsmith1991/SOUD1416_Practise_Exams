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

            // See how useful classes can be? :)
            // The ConsoleMenu() class can be easily reused in other projects, by just adding the ConsoleMenu.cs file to the solution!
            // Feel free to review ConsoleMenu()'s contents to see how this all works

            Console.WriteLine("\nProgram finished. Press enter to exit...");
            Console.ReadLine();
        }
    }
}
