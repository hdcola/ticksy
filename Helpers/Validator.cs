using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ticksy.Helpers
{
    public class Validator
    {
        /*
         * Validates a string based on minimum and maximum length.
         */
        public static bool ValidateInput(string input, string fieldName, int minLength, int maxLength, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                errorMessage = $"{fieldName} cannot be empty.";
                return false;
            }

            if (input.Length < minLength || input.Length > maxLength)
            {
                errorMessage = $"{fieldName} must be between {minLength} and {maxLength} characters.";
                return false;
            }

            return true;
        }


        public static bool ValidateEmail(string email, out string errorMessage)
        {   
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(email)) 
            {
                errorMessage = "Email cannot be empty.";
                return false;
            }


            // regex for email validation
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            if (!emailRegex.IsMatch(email))
            {
                errorMessage = "Please enter a valid email address.";
                return false;
            }

            return true;
        }


        public static void HandleValidationError(TextBlock errorTextBlock, string errorMessage, out bool isValid)
        {
            errorTextBlock.Text = errorMessage;
            Console.WriteLine(errorMessage);
            isValid = false;
        }


    }
}
