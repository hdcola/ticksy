using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticksy.Helpers
{
    public class Validator
    {
        /*
         * Validates a string based on minimum and maximum length.
         */
        public static bool ValidateString(string input, string fieldName, int minLength, int maxLength, out string errorMessage)
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




    }
}
