using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Strings {
    public static class Strings {
        // Removes specified characters from the given string
        public static string RemoveCharacters(this string input, char[] charactersToRemove) {
            return new string(input.Where(c => !charactersToRemove.Contains(c)).ToArray());
        }

        // Replaces specified substring with a new substring
        public static string ReplaceSubstring(this string input, string oldValue, string newValue) {
            return input.Replace(oldValue, newValue);
        }

        // Converts input string to TitleCase
        public static string ToTitleCase(this string input) {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
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

        // Detects encoding of the given string
        public static Encoding DetectEncoding(this string input) {
            byte[] bytes = Encoding.Default.GetBytes(input);

            if (bytes.Length >= 2 && bytes[0] == 0xff && bytes[1] == 0xfe)
            {
                return Encoding.Unicode;
            }

            if (bytes.Length >= 2 && bytes[0] == 0xfe && bytes[1] == 0xff)
            {
                return Encoding.BigEndianUnicode;
            }

            if (bytes.Length >= 3 && bytes[0] == 0xef && bytes[1] == 0xbb && bytes[2] == 0xbf)
            {
                return Encoding.UTF8;
            }

            return Encoding.Default;
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

        static bool IsPalindrome(this ReadOnlySpan<char> input)
        {
        	if (input.Length <= 1) return true;
        	
    		int e = input.Length;
   		    for (int s = 0; s < input.Length; s++)
   		    {
   		        e--;

   		        // Skip non-letter characters
   		        while (s < input.Length && !char.IsLetter(input[s])) s++;
                while (e >= 0 && !char.IsLetter(input[e])) e--;
                if (s >= input.Length || e < 0) break;

                if (input[s] != input[e]) return false;
   		    }
   		    
   		    return true;
        }

        public static bool IsPalindrome(this string input)
        {
        	return IsPalindrome(input.AsSpan());
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
