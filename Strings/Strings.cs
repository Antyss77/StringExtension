using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using PhoneNumbers;


namespace Strings {
    public static partial class Strings
    {

        /// <summary>
        /// Represents a regular expression that can be used to validate a mail address.
        /// </summary>
        /// <returns>A regular expression that can be used to validate a mail address.</returns>
        [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        private static partial Regex MailAddressRegex();
        
        /// <summary>
        /// Represents a regular expression that can be used to validate a phone number.
        /// </summary>
        /// <returns>A regular expression that can be used to validate a phone number.</returns>
        [GeneratedRegex(@"^(\+?\d{1,3}[\s-]?)?\(?\d{3}\)?[\s-]?\d{3}[\s-]?\d{4}$")]
        private static partial Regex PhoneNumberRegex();

        // Removes specified characters from the given string
        public static string RemoveCharacters(this string input, char[] charactersToRemove) {
            return new string(input.Where(c => !charactersToRemove.Contains(c)).ToArray());
        }

        // Validates email address
        [Obsolete("Use IsValidEmail instead")]
        public static bool IsValidEmailOld(this string email) {
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
        
        /// <summary>
        /// Validates the given email address.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns><c>true</c> if the given email address is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidEmail(this string email)
        {
            return !string.IsNullOrEmpty(email) && MailAddressRegex().IsMatch(email);
        }

        // Validates phone number
        [Obsolete("Use IsValidPhoneNumber instead")]
        public static bool IsValidPhoneNumberOld(this string phoneNumber) {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            // Regular expression to validate phone number format
            var regex = new Regex(@"^(\+?\d{1,3}[\s-]?)?\(?\d{3}\)?[\s-]?\d{3}[\s-]?\d{4}$");

            return regex.IsMatch(phoneNumber);
        }
        
        /// <summary>
        /// Validates the given phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate.</param>
        /// <returns><c>true</c> if the given phone number is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) && PhoneNumberRegex().IsMatch(phoneNumber);
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
        [Obsolete("Use ReverseWords instead")]
        public static string ReverseWordsOld(this string input) {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var words = input.Split(' ').Where(w => !string.IsNullOrEmpty(w)).ToArray();
            Array.Reverse(words);
            return string.Join(" ", words);
        }

        /// <summary>
        /// Reverses the order of words in the given string.
        /// </summary>
        /// <param name="input">The input to reverse words.</param>
        /// <returns>The input string with the order of words reversed.</returns>
        public static string ReverseWords(this string input)
        {
            var span = input.AsSpan();
            
            var builder = new StringBuilder();
            
            var start = span.Length;
            
            for (var i = span.Length - 1; i >= 0; i--)
            {
                if (span[i] is not ' ')
                    continue;
                
                builder
                    .Append(span.Slice(i + 1, start - i - 1))
                    .Append(' ');
                
                start = i;
            }
            
            builder.Append(span[..start]);
            
            return builder.ToString();
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
        [Obsolete("Use ToCamelCase instead")]
        public static string ConvertToCamelCaseOld(this string input) {
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
        
        /// <summary>
        /// Converts the given string to camel case.
        /// </summary>
        /// <param name="input">The input to transform.</param>
        /// <returns>The input string converted to camel case.</returns>
        public static string ToCamelCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            
            var span = input.AsSpan();

            Span<char> output = stackalloc char[input.Length];
            
            var outputIndex = 0;
            var shouldCapitalize = false;

            foreach (var c in span)
            {
                if (char.IsWhiteSpace(c) || c == '_')
                {
                    shouldCapitalize = true;
                }
                else
                {
                    if (shouldCapitalize)
                    {
                        output[outputIndex++] = char.ToUpperInvariant(c);
                        shouldCapitalize = false;
                    }
                    else
                    {
                        output[outputIndex++] = char.ToLowerInvariant(c);
                    }
                }
            }

            return new string(output[..outputIndex]);
        }
    }
}