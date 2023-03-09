using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Strings {
    public static class StringUtilities {
        public static string RemoveCharacters(string input, char[] charactersToRemove) {
            return new string(input.Where(c => !charactersToRemove.Contains(c)).ToArray());
        }

        public static string ReplaceSubstring(string input, string oldValue, string newValue) {
            return input.Replace(oldValue, newValue);
        }

        public static string ToTitleCase(string input) {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        }

        // Vérifie si l'adresse email est valide
        public static bool IsValidEmail(string email) {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            try
            {
                // Utilisation de la classe MailAddress pour valider l'adresse e-mail
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Vérifie si le numéro de téléphone est valide
        public static bool IsValidPhoneNumber(string phoneNumber) {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            // Expression régulière pour valider le format du numéro de téléphone
            var regex = new Regex(@"^\+\d{1,3}\s\d{1,14}$");

            return regex.IsMatch(phoneNumber);
        }

        // Convertit une chaîne de caractères de l'encodage donné en UTF-8
        public static string ConvertToUtf8(string input, Encoding encoding) {
            byte[] bytes = encoding.GetBytes(input);
            return Encoding.UTF8.GetString(bytes);
        }

        // Convertit une chaîne de caractères de l'encodage donné en UTF-16
        public static string ConvertToUtf16(string input, Encoding encoding) {
            byte[] bytes = encoding.GetBytes(input);
            return Encoding.Unicode.GetString(bytes);
        }

        // Détecte l'encodage de la chaîne de caractères donnée
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

