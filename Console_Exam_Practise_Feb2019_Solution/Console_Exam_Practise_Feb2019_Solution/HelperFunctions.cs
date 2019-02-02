using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;    // Required for working with files

namespace Console_Exam_Practise_Feb2019_Solution
{
    // Helper class containing various useful functions
    class HelperFunctions
    {
        // All objects are public so that we can access them from other classes

        // String constants
        public const string LETTERS = "abcdefghijklmnopqrstuvwxyz"; //As this string is for use with the below ValidateString() function, lets keep it within the same class
        public const char SPACE = ' ';

        // Function for validating the contents of a string
        // input_str : string to test
        // allowed_chars : character whitelist
        // case_sensitive : whether to compare strings in a case-sensitive manner i.e. a != A
        // case_sensitive parameter has a default value of 'false' if not specified when called
        // return type: bool (true if string contains only allowed chars, false if not)
        // public keyword means the function is accessible outside of the HelperFunctions class
        // static keyword means we don't need to instantiate the class to call the function i.e. helper_functions = new HelperFunctions()
        public static bool ValidateString(string input_str, string allowed_chars, bool case_sensitive = false)
        {
            if(!case_sensitive)
            {
                input_str = input_str.ToLower();    // Convert input_str to lower case, which allows letters that were uppercase to match characters in allowed_chars
            }

            // Use a 'foreach' statement to iterate through each character in input_str
            foreach(char c in input_str)
            {
                // Check if current input_str character is contained within allowed_chars
                // If not, exit the function and return false
                // Otherwise, move onto the next character (next foreach iteration)
                if(!allowed_chars.Contains(c))
                {
                    return false;
                }
            }

            return true;    // We've exited the foreach loop without encountering a non-allowed character. Therefore, return true
        }

        // Capitalise each word within the supplied string
        // input_str : the string to capitalise
        // return value : capitalised string (or empty string if supplied string contained no chars)
        public static string CapitaliseString(string input_str)
        {
            if(input_str.Length > 0)    // Ensure string contains some characters, to avoid out of bounds exceptions
            {
                // Create a char array copy of the supplied string. This enabled us to easily work with each char
                char[] input_str_chars = input_str.ToCharArray();

                // Capitalise the first character
                input_str_chars[0] = char.ToUpper(input_str_chars[0]);

                // Iterate through each remaining character in the string
                // Using a for loop instead of foreach as we want to modify chars. Foreach would only provide a copy of each char
                for (int i = 1; i < input_str_chars.Length; i++)
                {
                    // Was the previous character a space?
                    if(input_str_chars[i - 1] == ' ')
                    {
                        // Convert the current char to uppercase
                        input_str_chars[i] = char.ToUpper(input_str_chars[i]);
                    }
                }

                // Convert char array back to string, and return
                // Can't use .ToString() as this will return the name of the type (System.Char[])
                return new string(input_str_chars);
            }
            else
            {
                return string.Empty;
            }
        }

        // Writes an array of string values to a text file
        // path: location of written file
        // data: array of strings to write to file
        // overwrite: whether to overwrite the file if it exists
        public static bool WriteTextFile(string path, string[] data, bool overwrite)
        {
            // Use a try-catch statement to handle any exceptions caused by file write. Without this, the program will crash
            try
            {
                // If overwrite parameter is true, and file exists, delete file
                if(overwrite && File.Exists(path))
                {
                    File.Delete(path);
                }

                // Write data to file
                File.WriteAllLines(path, data);
            }
            catch
            {
                return false;   // Exception thrown, so return false
            }

            return true;    // Return true if file write succeeded
        }

        // Reads each line within a text file, returning a string array
        // path: location of text file
        // returns: array of string values, containing lines. Empty if file could not be read
        public static string[] ReadTextFile(string path)
        {
            try
            {
                if(File.Exists(path))   // Check file exists
                {
                    return File.ReadAllLines(path); // Read file, returning contents as a string array
                }
                else
                {
                    throw new Exception();  // Throws an exception, which enters the catch block
                }
            }
            catch
            {
                return new string[] { };   // Return empty string array
            }
        }
    }
}
