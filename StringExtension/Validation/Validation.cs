using System.Text.RegularExpressions;

namespace StringExtension.Validation;

/// <summary>
/// Provides extension methods for validating common string formats.
/// </summary>
public static partial class Validation
{
    /// <summary>
    /// Represents a regular expression that can be used to validate an email address.
    /// </summary>
    /// <returns>A regular expression that can be used to validate an email address.</returns>
    [GeneratedRegex(@"^[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+$")]
    private static partial Regex MailAddressRegex();

    /// <summary>
    /// Represents a regular expression that can be used to validate a phone number.
    /// </summary>
    /// <returns>A regular expression that can be used to validate a phone number.</returns>
    [GeneratedRegex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
    private static partial Regex PhoneNumberRegex();

    /// <summary>
    /// Validates the given email address.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
    /// <returns><c>true</c> if the given email address is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidEmail(this string email)
    {
        return !string.IsNullOrEmpty(email) && MailAddressRegex().IsMatch(email);
    }

    /// <summary>
    /// Validates the given email address.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
    /// <returns><c>true</c> if the given email address is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidEmail(this ReadOnlySpan<char> email)
    {
        return !email.IsEmpty && MailAddressRegex().IsMatch(email);
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

    /// <summary>
    /// Validates the given phone number.
    /// </summary>
    /// <param name="phoneNumber">The phone number to validate.</param>
    /// <returns><c>true</c> if the given phone number is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidPhoneNumber(this ReadOnlySpan<char> phoneNumber)
    {
        return !phoneNumber.IsEmpty && PhoneNumberRegex().IsMatch(phoneNumber);
    }
}