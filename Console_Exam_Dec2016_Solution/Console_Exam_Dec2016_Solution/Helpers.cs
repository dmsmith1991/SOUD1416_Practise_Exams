using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Console_Exam_Dec2016_Solution
{
    // Seperate class so it can be re-used by other classes. It's also neater :)
    class Helpers
    {
        // constant strings for char lists, to make using the 'validate_string' function easier
        public const string LETTER_CHARS = "abcdefghijklmnopqrstuvwxyz";
        public const string DIGIT_CHARS = "1234567890";

        // 'static' keyword means we can call this function without having to instantiate the 'Helpers' class
        public static bool validate_string(string input, string allowed_chars, bool case_sensitive = false)
        {
            if(!case_sensitive)
            {
                input = input.ToLower();    // Convert input string to lowercase for comparing against above string constants
            }

            foreach (char c in input)   // Iterate through each char in string 'input'
            {
                if (!allowed_chars.Contains(c))     // Does 'allowed_chars' string contain the current 'input' string char?
                {
                    return false;   // Char is not in allowed list, so return false (no point testing the other chars, so just return which exits now)
                }
            }

            return true;    // Got to the end, so there are no invalid chars. Therefore, we return true
        }

        public static void validate_string_example()
        {
            string string_to_test = "Hello World!";
            Console.WriteLine("string to test: " + string_to_test);
            bool string_valid = Helpers.validate_string(string_to_test, Helpers.LETTER_CHARS + Helpers.DIGIT_CHARS);
            Console.WriteLine("string valid: " + string_valid.ToString());
        }
    }
}
