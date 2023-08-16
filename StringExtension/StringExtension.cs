using System.Text;
using System.Text.RegularExpressions;


namespace StringExtension {
    public static partial class StringExtension {
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


        /// <summary>
        /// Validates the given email address.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns><c>true</c> if the given email address is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidEmail(this string email) {
            return !string.IsNullOrEmpty(email) && MailAddressRegex().IsMatch(email);
        }


        /// <summary>
        /// Validates the given phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate.</param>
        /// <returns><c>true</c> if the given phone number is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidPhoneNumber(this string phoneNumber) {
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

        /// <summary>
        /// Reverses the order of words in the given string.
        /// </summary>
        /// <param name="input">The input to reverse words.</param>
        /// <returns>The input string with the order of words reversed.</returns>
        public static string ReverseWords(this string input) {
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

        /// <summary>
        /// Converts the given string to camel case.
        /// </summary>
        /// <param name="input">The input to transform.</param>
        /// <returns>The input string converted to camel case.</returns>
        public static string ToCamelCase(this string input) {
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