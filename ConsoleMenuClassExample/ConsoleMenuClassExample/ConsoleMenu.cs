using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleMenuClassExample
{
    // Class for building and outputting custom console-based menus
    class ConsoleMenu
    {
        #region Global Variables
        // These variables are used internally by this class (ConsoleMenu), and are not directly accessible from other classes (private keyword)
        // This prevents direct modification of the values, which could break the class functionality
        // Example: AddOption() checks if menu_options contains the supplied option string, before adding it to this list. If menu_options was public, other classes could bypass these checks by adding values to the list directly
        // This is common practise, to ensure classes can only be used as intended
        private List<string> menu_options;  // List to store our menu option strings
        private string menu_prompt;         // String variable to store menu prompt text
        #endregion

        #region Properties
        // Property for reading / writing of the menu prompt text
        public string PromptText
        {
            get // Handle a get of the property value
            {
                return menu_prompt;
            }
            set // Handle setting of the property
            {
                if(!string.IsNullOrEmpty(value)) // Ensure the value is worth storing
                {
                    menu_prompt = value;    // Set menu_prompt as the supplied value
                }
            }
        }
        #endregion

        #region Methods
        // Constructor method for this class. This gets called automatically on new instance creation (new keyword usage, e.g. ConsoleMenu my_menu = new ConsoleMenu())
        public ConsoleMenu()
        {
            // Instantiate menu_options string List
            menu_options = new List<string>();
            // Default menu prompt text
            menu_prompt = "Please select from one of the following options:";
        }

        // Method for adding a single option to the menu
        // option_text: text to be shown when this option is listed
        // return type: bool (true if option was added, false if already exists)
        public bool AddOption(string option_str)
        {
            if(!menu_options.Contains(option_str))  // Does menu_options already contain this value?
            {
                menu_options.Add(option_str);   // Add value to menu_options List
                return true;    // Option was added
            }
            else
            {
                return false;   // menu_options already contains supplied value, so return false
            }
        }

        // Method for adding multiple options to the menu (with one function call)
        // option_strs: string array of option strings to add
        // return type: bool (true if all options were added, false if encountered one that already exists)
        public bool AddOptions(string[] option_strs)
        {
            // Add each supplied option string, each time checking that they don't already exist
            foreach(string str in option_strs)  // Using a foreach to iterate through each value, as we just need a copy of the value to add
            {
                if(!AddOption(str)) // We already have a method within this class for adding a single option value, so lets just use that
                {
                    return false;   // AddOption() indicated option already exists (returned false), so return false
                }
            }

            return true;    // Return true if all supplied options were successfully added
        }

        // Method for removing existing options from the menu
        // return type: bool (true if option was removed, false if doesn't exist)
        public bool RemoveOption(string option_text)
        {
            // See AddOption(), which the below basically does the opposite to

            if(menu_options.Contains(option_text))
            {
                menu_options.Remove(option_text);
                return true;
            }
            else
            {
                return false;
            }
        }

        // Clear all existing menu options
        // No params or return value (this will always succeed)
        public void ClearOptions()
        {
            menu_options.Clear();
        }

        // Shows the menu, returning the user selected option once chosen
        // Params: none
        // Return type: string (selected option string, or null if user exited)
        public string Run()
        {
            if(menu_options.Count > 1)  // Ensure menu has at least 2 options, otherwise what's the point in continuing
            {
                while (true) // Keep looping until the user has either provided a valid input or exited the menu
                {
                    Console.WriteLine("\n" + menu_prompt);  // Show the menu prompt text, prepended with a newline
                    // List the menu options. Lists keep values in the order they were added (top to bottom -> first to last)
                    // Using a for loop means we can just +1 to the current list index, to 'calculate' the option's number in the list
                    for (int i = 0; i < menu_options.Count; i++)
                    {
                        Console.WriteLine("{0}) {1}", i + 1, menu_options[i]);
                    }
                    // Show skip message
                    Console.WriteLine("To exit, just press enter");
                    // Ask user for their selection
                    Console.Write("Enter selection (number): ");
                    string user_input = Console.ReadLine();
                    // Check if user wishes to exit (pressed enter without supplying a value)
                    if (user_input == string.Empty)
                    {
                        return null;    // Return null to indicate to the calling function that the user has opted to exit
                    }
                    // Try and parse the entered value (string) as an integer, which enables us to perform a numerical range check (see further down)
                    int option_num;
                    if (int.TryParse(user_input, out option_num))   // See https://www.dotnetperls.com/parse, TryParse section for specifics
                    {
                        // The following check ensures that the user-supplied option number falls within the range of those shown
                        if (option_num > 0 && option_num <= menu_options.Count)
                        {
                            return menu_options[option_num - 1];    // Lookup and return chosen option string. -1 to convert number back to zero-base index, otherwise we'll be fetching the option after the one selected
                        }
                    }

                    // If we've gotten this far, then the user input was found to be invalid. We therefore need to stay within in the while loop (shows the menu again)
                    Console.WriteLine("\nInvalid selection, please try again");
                }
            }
            else
            {
                throw new Exception("ConsoleMenu->Show() failed: insufficient menu options"); // Throw an exception, as the class is being used incorrectly
            }
        }
        #endregion
    }
}
