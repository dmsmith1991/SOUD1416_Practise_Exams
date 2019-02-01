using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Console_Exam_Dec2016_Solution
{
    class Program
    {
        private enum program_states_t
        {
            main_menu,
            add_user,
            show_user
        }

        private enum add_user_states_t
        {
            initial,
            surname,
            age,
            weight,
            finished
        }

        // Object to encapsulate user data values
        // Using a class instead of struct as I'd like to have defaulted values on instantiation
        private class user_data_t
        {
            public char? initial = null;
            public string surname = string.Empty;
            public int age = 0;                     // int is fine as we aren't going above 2^31
            public int weight = 0;                  // int is again fine

            public bool complete
            {
                get
                {
                    return (initial != null && surname != string.Empty && age != 0 && weight != 0);
                }
            }
        }

        static void Main(string[] args)
        {
            program_states_t current_state = program_states_t.main_menu;    // Enum variable for current program state (menu)
            add_user_states_t add_user_state = add_user_states_t.initial;   // First section of data entry process is initial
            user_data_t user_data = new user_data_t();  // Using 'new' keyword as user_data_t is a class

            string user_input;  // Declared global as we'll be using it constantly (personal preference)

            while (true)
            {
                switch(current_state)
                {
                    /* Main Menu State */
                    case program_states_t.main_menu:
                        if(user_data.complete)
                        {
                            ShowBilly();    // Let's make this program a little less fucking boring...
                        }

                        // Show menu options
                        Console.WriteLine("\nPlease type one of the following options and press Enter:");
                        Console.WriteLine("1) Add user");
                        Console.WriteLine("2) Show user");
                        Console.WriteLine("3) Exit");

                        if(user_data.complete)
                        {
                            Console.Write("Will you escape from this pointless program? Make your choice: ");
                        }
                        else
                        {
                            Console.Write("Option: ");
                        }
                        
                        // Get user input
                        user_input = Console.ReadLine();
                        switch(user_input)
                        {
                            case "1":   // Move to add user state
                                current_state = program_states_t.add_user;
                                break;
                            case "2":   // Move to show user state
                                current_state = program_states_t.show_user;
                                break;
                            case "3":   // Exit program
                                Console.WriteLine("\nThank you for using this program. Good Bye");
                                Console.WriteLine("Press Enter to exit console");
                                Console.ReadLine();
                                return; // Exits main (current function), and therefore program closes
                            default:    // Anything else i.e. invalid user input
                                Console.WriteLine("[!] Invalid option, please try again");
                                break;
                        }
                        break;  // Break for this program state case

                    /* Add User State */
                    case program_states_t.add_user:
                        switch (add_user_state)
                        {
                            case add_user_states_t.initial:
                                Console.Write("\nPlease enter an initial: ");
                                // Get user input
                                user_input = Console.ReadLine();
                                // Validate user input (initial should be a single uppercase letter)
                                if(user_input.Length == 1 && Helpers.validate_string(user_input, Helpers.LETTER_CHARS.ToUpper(), true))
                                {
                                    // 'user_input' may indeed contain a single character, but it is still a string datatype. Needs converting
                                    user_data.initial = user_input.ToCharArray()[0];    // Convert 'user_input' string to char array, then grab the first char (index 0)
                                    add_user_state = add_user_states_t.surname;         // Move to next add user state
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input, please try again.");
                                }
                                break;
                            case add_user_states_t.surname:
                                Console.Write("\nPlease enter a surname: ");
                                // Get user input
                                user_input = Console.ReadLine();
                                // Validate user input
                                // length > 1 and only letters, case insensitive (see validate_string() param 3)
                                if(user_input.Length > 1 && Helpers.validate_string(user_input, Helpers.LETTER_CHARS))
                                {
                                    user_data.surname = user_input;
                                    add_user_state = add_user_states_t.age;     // Move to next section
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input, please try again.");
                                }
                                break;
                            case add_user_states_t.age:
                                Console.Write("\nPlease enter an age: ");
                                // Get user input
                                user_input = Console.ReadLine();
                                // Validate user input (length > 1 and only numeric chars)
                                // You could also use int.TryParse() to handle signing & truncate decimal values
                                if (user_input.Length > 1 && Helpers.validate_string(user_input, Helpers.DIGIT_CHARS))
                                {
                                    user_data.age = Convert.ToInt32(user_input);    // Directly cast 'user_input' string to int. We know this will succeed as we've already validated the string contains only numbers
                                    add_user_state = add_user_states_t.weight;      // Move to next section
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input, please try again.");
                                }
                                break;
                            case add_user_states_t.weight:
                                Console.Write("\nPlease enter a weight in whole kilograms: ");  // You could be a little more adventurous here and allow users to supply a unit e.g. 100kg, 100lb, but for now this will suffice
                                // Get user input
                                user_input = Console.ReadLine();
                                // Validate user input (length > 0 and only numeric chars)
                                if (user_input.Length > 0 && Helpers.validate_string(user_input, Helpers.DIGIT_CHARS))
                                {
                                    user_data.weight = Convert.ToInt32(user_input);     // Cast 'user_input' string to int. We know this will succeed without throwing an exception, as we've already validated the string contains only numbers
                                    add_user_state = add_user_states_t.finished;        // Move to finish state
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input, please try again.");
                                }
                                break;
                            case add_user_states_t.finished:
                                Console.WriteLine("\nUser record updated! Returning to main menu...");
                                current_state = program_states_t.main_menu;     // Return to main menu
                                break;
                        }
                        break;

                    /* Show User State */
                    case program_states_t.show_user:
                        if(user_data.complete)   // Check if user_data object is fully populated with values
                        {
                            Console.WriteLine("\nInitial: " + user_data.initial);
                            Console.WriteLine("Surname: " + user_data.surname);
                            Console.WriteLine("Age: " + user_data.age.ToString());
                            Console.WriteLine(string.Format("Weight: {0}kg", user_data.weight));
                        }
                        else
                        {
                            Console.WriteLine("\nError: user data is incomplete");
                        }
                        current_state = program_states_t.main_menu;     // Return to main menu

                        break;
                }
            }
        }

        static void ShowBilly()
        {
            Console.Write("\n");
            Console.WriteLine("I WANT TO PLAY A GAME...\n");
            Console.WriteLine("─────▄██▀▀▀▀▀▀▀▀▀▀▀▀▀██▄─────");
            Console.WriteLine("────███───────────────███────");
            Console.WriteLine("───███─────────────────███───");
            Console.WriteLine("──███───▄▀▀▄─────▄▀▀▄───███──");
            Console.WriteLine("─████─▄▀────▀▄─▄▀────▀▄─████─");
            Console.WriteLine("─████──▄████─────████▄──█████");
            Console.WriteLine("█████─██▓▓▓██───██▓▓▓██─█████");
            Console.WriteLine("█████─██▓█▓██───██▓█▓██─█████");
            Console.WriteLine("█████─██▓▓▓█▀─▄─▀█▓▓▓██─█████");
            Console.WriteLine("████▀──▀▀▀▀▀─▄█▄─▀▀▀▀▀──▀████");
            Console.WriteLine("███─▄▀▀▀▄────███────▄▀▀▀▄─███");
            Console.WriteLine("███──▄▀▄─█──█████──█─▄▀▄──███");
            Console.WriteLine("███─█──█─█──█████──█─█──█─███");
            Console.WriteLine("███─█─▀──█─▄█████▄─█──▀─█─███");
            Console.WriteLine("███▄─▀▀▀▀──█─▀█▀─█──▀▀▀▀─▄███");
            Console.WriteLine("████─────────────────────████");
            Console.WriteLine("─███───▀█████████████▀───████");
            Console.WriteLine("─███───────█─────█───────████");
            Console.WriteLine("─████─────█───────█─────█████");
            Console.WriteLine("───███▄──█────█────█──▄█████─");
            Console.WriteLine("─────▀█████▄▄███▄▄█████▀─────");
            Console.WriteLine("──────────█▄─────▄█──────────");
            Console.WriteLine("──────────▄█─────█▄──────────");
            Console.WriteLine("───────▄████─────████▄───────");
            Console.WriteLine("─────▄███████───███████▄─────");
            Console.WriteLine("───▄█████████████████████▄───");
            Console.WriteLine("─▄███▀───███████████───▀███▄─");
            Console.WriteLine("███▀─────███████████─────▀███");
            Console.WriteLine("▌▌▌▌▒▒───███████████───▒▒▐▐▐▐");
            Console.WriteLine("─────▒▒──███████████──▒▒─────");
            Console.WriteLine("──────▒▒─███████████─▒▒──────");
            Console.WriteLine("───────▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒───────");
            Console.WriteLine("─────────████░░█████─────────");
            Console.WriteLine("────────█████░░██████────────");
            Console.WriteLine("──────███████░░███████───────");
            Console.WriteLine("─────█████──█░░█──█████──────");
            Console.WriteLine("─────█████──████──█████──────");
            Console.WriteLine("──────████──████──████───────");
            Console.WriteLine("──────████──████──████───────");
            Console.WriteLine("──────████───██───████───────");
            Console.WriteLine("──────████───██───████───────");
            Console.WriteLine("──────████──████──████───────");
            Console.WriteLine("─██────██───████───██─────██─");
            Console.WriteLine("─██───████──████──████────██─");
            Console.WriteLine("─███████████████████████████─");
            Console.WriteLine("─██─────────████──────────██─");
            Console.WriteLine("─██─────────████──────────██─");
            Console.WriteLine("────────────████─────────────");
            Console.WriteLine("─────────────██──────────────");
            Console.Write("\n");
        }
    }
}
