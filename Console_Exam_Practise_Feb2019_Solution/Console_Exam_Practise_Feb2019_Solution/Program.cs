using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;    // Required for working with files

namespace Console_Exam_Practise_Feb2019_Solution
{
    class Program
    {
        // Constant value for customer names file path, which saves us having to type this string out multiple times
        // Private as it doesn't need to be accessed from outside of the class
        // This ensures all parts of the code that read/write to this file with use the exact same path
        // No absolute path supplied (C:\ etc.), so file will be located in same directory as program executable
        private const string CUSTOMER_NAMES_FILE_PATH = "customer_names.txt";

        static void Main(string[] args)
        {
            // Global variables (scope: only accessible within Main())
            bool exit_program = false;  // Flag variable to exit while loop (break action can't be used due to decision to exit program being made within a switch-case statement, so a flag is required)
            List<string> customer_names = new List<string>();   // List object of type string, for storing of names. A list object can dynamically grow is size as required, which avoid using fixed size arrays which may be insufficient in size

            // Program welcome message. This will only execute once as it's before the main menu loop
            Console.WriteLine("Welcome!");

            // If customer data has been saved from a previous run of the program, retrieve this and load into customer_names list
            // customer_names is a string List, ReadTextFile() returns a string array, so a conversion is required (.ToList method)
            customer_names = HelperFunctions.ReadTextFile(CUSTOMER_NAMES_FILE_PATH).ToList<string>();
            /*
            // Output to the console if we've loaded names from a previous session
            if (customer_names.Count > 0)
            {
                Console.WriteLine("\nLoaded {0} customers from a previous session.", customer_names.Count);
            }
            */

            // Start the main menu loop
            while (!exit_program)
            {
                string user_input;  // We'll be using this variable throughout, so let's just declare it here

                // Output main menu header
                Console.WriteLine("\nPlease select from one of the following options:");
                // Show menu options
                Console.WriteLine("1) Add customer");
                Console.WriteLine("2) Show existing customers");
                Console.WriteLine("3) Run calculator");
                Console.WriteLine("4) Quit program");
                // Ask user for their selection. Console.Write() keeps everything on one line
                Console.Write("Enter selection (number): ");
                user_input = Console.ReadLine(); // Return typed string after user presses enter key
                // Act on user input
                // Using a switch-case statement for neater branching
                switch(user_input)
                {
                    case "1":
                        /* ADD NEW CUSTOMER CODE BLOCK */
                        while (true)    // This while loops allows us to keep asking the user for new input if the previous doesn't meet our requirements
                        {
                            // Get name from user
                            Console.Write("\nPlease enter the customer name: ");
                            user_input = Console.ReadLine();    // We can safely reuse user_input, as we have finished with its previous value. For begineers it's not a bad idea to declare new variables to avoid confusion
                            // Validate user input. Names should only contain letters (case-insensitive, default parameter), spaces and hiphens
                            if (HelperFunctions.ValidateString(user_input, HelperFunctions.LETTERS + " -"))
                            {
                                // Fancy feature: ensure the name is stored capitalised. This isn't a requirement of the brief, just a nice to have
                                // Review HelperFunctions -> CapitaliseString() if you'd like to see how this works
                                string name = HelperFunctions.CapitaliseString(user_input);

                                // Only add the name if it doesn't already exist.
                                // Exit back to main menu regardless, incase user has made a mistake
                                if(!customer_names.Contains(name))
                                {
                                    customer_names.Add(name); // Add user supplied string to the customer_names list

                                    Console.WriteLine("Name added. Returning to main menu...");
                                }
                                else
                                {
                                    Console.WriteLine("Name already exists, so will not be added. Returning to main menu...");
                                }

                                break;  // Break out of this while loop, handing back control to main menu loop
                            }
                            else
                            {
                                // Inputted name doesn't pass our validation requirements, so communicate this to the user
                                Console.WriteLine("ERROR: Invalid name '{0}', please try again", user_input);
                                // Program will now perform another iteration of the new customer while loop
                            }
                        }
                        /* END OF ADD NEW CUSTOMER CODE BLOCK */
                        break;  // this breaks out of the switch-case, NOT the while loop
                    case "2":
                        /* SHOW EXISTING CUSTOMERS CODE BLOCK */
                        Console.WriteLine("\nThere are {0} existing customers", customer_names.Count);    // Output the number of stored customers
                        // Using a for loop instead of for each, as this allows us to output the customer name position also (nice to have)
                        for (int i = 0; i < customer_names.Count; i++)
                        {
                            // Values stored within List objects (in this case, customer_name) can be accessed just like an array (square brackets)
                            // WriteLine() supports string formatting (just like with string.Format()). This saves us from having to perform multiple concatenations e.g. "123" + "456" + "789"
                            Console.WriteLine("Customer #{0}: {1}", i + 1, customer_names[i]);
                        }
                        /* END OF SHOW EXISTING CUSTOMERS CODE BLOCK */
                        break;  // this breaks out of the switch-case, NOT the while loop
                    case "3":
                        RunCalculator();    // Run calculator feature. Implemented within a seperate function as this features requires a larger block of code (personal preference, I find this to be neater)
                        break;  // this breaks out of the switch-case, NOT the while loop
                    case "4":
                        // Before we exit, save off customer list to a text file
                        if(customer_names.Count > 0)    // Are there names to save?
                        {
                            // Try and write the customer data to a text file, continuing with program exit if succeeds
                            if(HelperFunctions.WriteTextFile(CUSTOMER_NAMES_FILE_PATH, customer_names.ToArray(), true))
                            {
                                // Set 'exit_program' flag, so that the main menu while loop exits on the next iteration
                                exit_program = true;
                            }
                            else
                            {
                                // Output an error message, and don't set exit_program flag so we stay in the main menu while loop
                                Console.WriteLine("ERROR: Failed to save customer data. Program will NOT exit.");
                            }
                        }
                        else
                        {
                            // No customer names to save, so just exit

                            // Set 'exit_program' flag, so that the main menu while loop exits on the next iteration
                            exit_program = true;
                        }
                        break;  // this breaks out of the switch-case, NOT the while loop
                    default:
                        // Below code is executed if none of the above conditions for 'user_input' are met
                        Console.WriteLine("ERROR: Invalid selection, please try again");
                        break;  // this breaks out of the switch-case, NOT the while loop
                }
            }

            // Goodbye message
            Console.WriteLine("\nGoodbye. Press enter to exit");
            // Keep console window open until user presses enter key
            Console.ReadLine();
        }

        // Calculator feature, coded within it's own function
        // Return type is void as we have no need to return a value to the calling scope
        static void RunCalculator()
        {
            bool exit_calc = false; // Flag to break out of while loop from within the switch-case (see main menu code for further explanation)
            string user_input;      // String variable for storing user input. A variable of the same name is indeed declared within Main(), but as it's not global (class-wide) this variable isn't accessible within RunCalculator(). Therefore, there is no error for the declaration on this line

            Console.WriteLine("\nRunning calculator...");

            while(!exit_calc)
            {
                // Print menu options
                Console.WriteLine("\nPlease select from one of the following options:");
                Console.WriteLine("1) Perform a calculation");
                Console.WriteLine("2) Return to main menu");
                // Request option from user
                Console.Write("Enter selection (number): ");
                user_input = Console.ReadLine();
                // Act on user input (switch-case as with the main menu)
                switch(user_input)
                {
                    case "1":
                        /* PERFORM CALCULATION CODE BLOCK */
                        Console.WriteLine("");  // New line for neatness

                        int[] numbers = new int[2]; // Array to store user-supplied numbers

                        // Populate numbers array with user input. Request a number per array slot.
                        // Using a for loop and array of ints prevents the need for copying and pasting code!
                        for(int i = 0; i < numbers.Length; i++)
                        {
                            while (true)    // Stay in the current for loop iteration until we've received valid user input
                            {
                                Console.Write("Please enter number #{0}: ", i + 1);
                                user_input = Console.ReadLine();
                                // Try and parse user input into numbers array, erroring if failed
                                // Using TryParse() as this handles everything for us
                                // Explanation: https://www.tutorialspoint.com/chash-int-tryparse-method
                                if (int.TryParse(user_input, out numbers[i]))
                                {
                                    break;  // Exit the while loop and move to next for iteration, as we've been given a valid input
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: Please enter a valid 32-bit signed integer");
                                }
                            }
                        }

                        int? answer = null; // Variable to store calculated answer. Question mark means we want an nullable int type (can be set to null)
                        char operator_char = '\0'; // Variable to store operator. \0 is just a temporary value (byte value 0)
                        while (true)
                        {
                            // Request desired mathematical operation from user, via a menu
                            Console.WriteLine("\nPlease select from one of the below mathematical operations:");
                            Console.WriteLine("1) Add");
                            Console.WriteLine("2) Subtract");
                            Console.WriteLine("3) Multiply");
                            Console.WriteLine("4) Divide");
                            // Ask user for their selection. Console.Write() keeps everything on one line
                            Console.Write("Enter selection (number): ");
                            user_input = Console.ReadLine(); // Return typed string after user presses enter key
                            // Calculate answer using supplied numbers and operator
                            switch (user_input)
                            {
                                case "1":
                                    answer = numbers[0] + numbers[1];
                                    operator_char = '+';
                                    break;
                                case "2":
                                    answer = numbers[0] - numbers[1];
                                    operator_char = '-';
                                    break;
                                case "3":
                                    answer = numbers[0] * numbers[1];
                                    operator_char = 'x';
                                    break;
                                case "4":
                                    answer = numbers[0] / numbers[1];
                                    operator_char = '/';
                                    break;
                                default:
                                    Console.WriteLine("ERROR: Invalid selection, please try again");
                                    break;
                            }
                            // If answer has been set (valid operation option selected), exit the while loop
                            if (answer != null)
                            {
                                break;  // Exit menu while loop (we're not in a switch-case etc.)
                            }
                        }
                        // Output the entire equation & answer
                        Console.WriteLine("\nResult: {0} {1} {2} = {3}", numbers[0], operator_char, numbers[1], answer);
                        /* END OF PERFORM CALCULATION CODE BLOCK */
                        break;  // this breaks out of the switch-case, NOT the while loop
                    case "2":
                        exit_calc = true;   // Set the exit_calc flag, so we exit the while loop and therefore return from the function
                        break;  // this breaks out of the switch-case, NOT the while loop
                    default:
                        Console.WriteLine("ERROR: Invalid selection, please try again");
                        break;
                }
            }

            Console.WriteLine("Exiting calculator, returning to main menu...");
        }
    }
}
