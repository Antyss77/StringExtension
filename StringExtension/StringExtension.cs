using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using PhoneNumbers;


namespace Strings {
    public static class Strings {
        // Removes specified characters from the given string
        public static string RemoveCharacters(this string input, char[] charactersToRemove) {
            return new string(input.Where(c => !charactersToRemove.Contains(c)).ToArray());
        }

        // Validates email address
        public static bool IsValidEmail(this string email) {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            try
            {
                // Use MailAddress class to validate email address
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Validates phone number
        public static bool IsValidPhoneNumber(this string phoneNumber) {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            // Regular expression to validate phone number format
            var regex = new Regex(@"^(\+?\d{1,3}[\s-]?)?\(?\d{3}\)?[\s-]?\d{3}[\s-]?\d{4}$");

            return regex.IsMatch(phoneNumber);
        }

        // Counts the number of occurrences of a substring in the given string
        public static int CountSubstring(this string input, string substring) {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(substring))
            {
                return 0;
            }

            return (input.Length - input.Replace(substring, "").Length) / substring.Length;
        }

        // Reverses the order of words in the given string
        public static string ReverseWords(this string input) {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var words = input.Split(' ').Where(w => !string.IsNullOrEmpty(w)).ToArray();
            Array.Reverse(words);
            return string.Join(" ", words);
        }

        // Determines if the given string is a palindrome
        public static bool IsPalindrome(this string input) {
            // Remove all non-letter characters and convert to lowercase
            string cleanString = new string(input.Where(char.IsLetter).Select(char.ToLower).ToArray());

            // Check if the string is equal to its reverse
            return cleanString == new string(cleanString.Reverse().ToArray());
        }


        // Counts the number of letters in the given string
        public static int CountLetters(this string input) {
            // Remove all non-letter characters and count the length of the resulting string
            return input.Count(char.IsLetter);
        }

        // Removes duplicate characters from the given string
        public static string RemoveDuplicateCharacters(this string input) {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Convert input to char array and remove duplicates
            var chars = input.ToCharArray().Distinct().ToArray();

            // Convert char array back to string
            return new string(chars);
        }

        // Converts input string to camel case
        public static string ConvertToCamelCase(this string input) {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Convert input to lowercase and split by whitespace or underscore
            var words = input.ToLower().Split(new[] {' ', '_'}, StringSplitOptions.RemoveEmptyEntries);

            // Capitalize first letter of each word except the first one
            for (int i = 1; i < words.Length; i++)
            {
                var chars = words[i].ToCharArray();
                chars[0] = char.ToUpper(chars[0]);
                words[i] = new string(chars);
            }

            // Join words and return result
            return string.Join("", words);
        }
    }
}