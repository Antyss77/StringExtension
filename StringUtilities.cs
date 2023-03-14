using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Strings {
    public static class StringUtilities {
        // Removes specified characters from the given string
        public static string RemoveCharacters(string input, char[] charactersToRemove) {
            return new string(input.Where(c => !charactersToRemove.Contains(c)).ToArray());
        }

        // Replaces specified substring with a new substring
        public static string ReplaceSubstring(string input, string oldValue, string newValue) {
            return input.Replace(oldValue, newValue);
        }

        // Converts input string to title case
        public static string ToTitleCase(string input) {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        }

        // Validates email address
        public static bool IsValidEmail(string email) {
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
        public static bool IsValidPhoneNumber(string phoneNumber) {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            // Regular expression to validate phone number format
            var regex = new Regex(@"^\+\d{1,3}\s\d{1,14}$");

            return regex.IsMatch(phoneNumber);
        }

        // Converts input string from the given encoding to UTF-8
        public static string ConvertToUtf8(string input, Encoding encoding) {
            byte[] bytes = encoding.GetBytes(input);
            return Encoding.UTF8.GetString(bytes);
        }

        // Converts input string from the given encoding to UTF-16
        public static string ConvertToUtf16(string input, Encoding encoding) {
            byte[] bytes = encoding.GetBytes(input);
            return Encoding.Unicode.GetString(bytes);
        }

        // Detects encoding of the given string
        public static Encoding DetectEncoding(string input) {
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
    }
}
