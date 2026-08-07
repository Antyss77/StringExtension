using System.Buffers;
using System.Text.RegularExpressions;

namespace StringExtension;

/// <summary>
/// Provides extension methods for string manipulation.
/// </summary>
public static partial class StringExtension
{
    /// <summary>
    /// Above this length, buffers are rented from <see cref="ArrayPool{T}"/> instead
    /// of stack-allocated, to avoid excessive stack usage for large inputs.
    /// </summary>
    private const int StackAllocThreshold = 256;

    /// <summary>
    /// Represents a regular expression that can be used to validate an email address.
    /// </summary>
    /// <returns>A regular expression that can be used to validate an email address.</returns>
    [GeneratedRegex(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+")]
    private static partial Regex MailAddressRegex();

    /// <summary>
    /// Represents a regular expression that can be used to validate a phone number.
    /// </summary>
    /// <returns>A regular expression that can be used to validate a phone number.</returns>
    [GeneratedRegex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
    private static partial Regex PhoneNumberRegex();

    /// <summary>
    /// Removes specified characters from the given string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="charactersToRemove">An array of characters to remove.</param>
    /// <returns>A new string with specified characters removed.</returns>
    /// <remarks>Returns <see langword="null"/> if <paramref name="input"/> is <see langword="null"/>.</remarks>
    public static string RemoveCharacters(this string input, char[] charactersToRemove)
    {
        if (string.IsNullOrEmpty(input) || charactersToRemove is null || charactersToRemove.Length == 0)
        {
            return input;
        }

        return RemoveCharacters(input.AsSpan(), charactersToRemove);
    }

    /// <summary>
    /// Removes specified characters from the given span of characters.
    /// </summary>
    /// <param name="input">The input characters.</param>
    /// <param name="charactersToRemove">The characters to remove.</param>
    /// <returns>A new string with specified characters removed.</returns>
    public static string RemoveCharacters(this ReadOnlySpan<char> input, ReadOnlySpan<char> charactersToRemove)
    {
        if (input.IsEmpty || charactersToRemove.IsEmpty)
        {
            return input.ToString();
        }

        char[]? pooledBuffer = null;
        Span<char> buffer = (uint)input.Length <= StackAllocThreshold
            ? stackalloc char[input.Length]
            : (pooledBuffer = ArrayPool<char>.Shared.Rent(input.Length));

        try
        {
            var count = 0;
            foreach (var c in input)
            {
                if (charactersToRemove.IndexOf(c) < 0)
                {
                    buffer[count++] = c;
                }
            }

            return new string(buffer[..count]);
        }
        finally
        {
            if (pooledBuffer is not null)
            {
                ArrayPool<char>.Shared.Return(pooledBuffer);
            }
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

    /// <summary>
    /// Counts the number of occurrences of a substring in the given string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="substring">The substring to count.</param>
    /// <returns>The number of occurrences of the substring in the input string.</returns>
    public static int CountSubstring(this string input, string substring)
    {
        return CountSubstring(input.AsSpan(), substring.AsSpan());
    }

    /// <summary>
    /// Counts the number of occurrences of a substring in the given span of characters.
    /// Zero-allocation: delegates directly to <see cref="MemoryExtensions.Count{T}(ReadOnlySpan{T}, ReadOnlySpan{T})"/>.
    /// </summary>
    /// <param name="input">The input characters.</param>
    /// <param name="substring">The characters to count.</param>
    /// <returns>The number of occurrences of the substring in the input.</returns>
    public static int CountSubstring(this ReadOnlySpan<char> input, ReadOnlySpan<char> substring)
    {
        if (input.IsEmpty || substring.IsEmpty)
        {
            return 0;
        }

        return input.Count(substring);
    }

    /// <summary>
    /// Reverses the order of words in the given string.
    /// </summary>
    /// <param name="input">The input to reverse words.</param>
    /// <returns>The input string with the order of words reversed.</returns>
    /// <remarks>Returns <see langword="null"/> if <paramref name="input"/> is <see langword="null"/>.</remarks>
    public static string ReverseWords(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        return string.Create(input.Length, input, static (span, source) =>
        {
            foreach (Range word in source.AsSpan().Split(' '))
            {
                ReadOnlySpan<char> wordSpan = source.AsSpan()[word];
                var length = wordSpan.Length;

                // Copy the word to the end of the remaining span.
                wordSpan.TryCopyTo(span[^length..]);
                span = span[..^length];

                // Insert a separating space, unless this was the last word.
                if (!span.IsEmpty)
                {
                    span[^1] = ' ';
                    span = span[..^1];
                }
            }
        });
    }

    /// <summary>
    /// Determines if the given string is a palindrome.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns><c>true</c> if the string is a palindrome; otherwise, <c>false</c>.</returns>
    public static bool IsPalindrome(this string input)
    {
        return IsPalindrome(input.AsSpan());
    }

    /// <summary>
    /// Determines if the given span of characters is a palindrome.
    /// Zero-allocation: compares from both ends inward, skipping non-letters.
    /// </summary>
    /// <param name="input">The input characters.</param>
    /// <returns><c>true</c> if the characters form a palindrome; otherwise, <c>false</c>.</returns>
    public static bool IsPalindrome(this ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            return false;
        }

        var left = 0;
        var right = input.Length - 1;

        while (left < right)
        {
            if (!char.IsLetter(input[left]))
            {
                left++;
                continue;
            }

            if (!char.IsLetter(input[right]))
            {
                right--;
                continue;
            }

            if (char.ToLowerInvariant(input[left]) != char.ToLowerInvariant(input[right]))
            {
                return false;
            }

            left++;
            right--;
        }

        return true;
    }

    /// <summary>
    /// Counts the number of letters in the given string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The number of letters in the input string.</returns>
    public static int CountLetters(this string input)
    {
        return CountLetters(input.AsSpan());
    }

    /// <summary>
    /// Counts the number of letters in the given span of characters.
    /// </summary>
    /// <param name="input">The input characters.</param>
    /// <returns>The number of letters in the input.</returns>
    public static int CountLetters(this ReadOnlySpan<char> input)
    {
        var count = 0;
        foreach (var c in input)
        {
            if (char.IsLetter(c))
            {
                count++;
            }
        }

        return count;
    }

    /// <summary>
    /// Removes duplicate characters from the given string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>A new string with duplicate characters removed.</returns>
    /// <remarks>Returns <see langword="null"/> if <paramref name="input"/> is <see langword="null"/>.</remarks>
    public static string RemoveDuplicateCharacters(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        return RemoveDuplicateCharacters(input.AsSpan());
    }

    /// <summary>
    /// Removes duplicate characters from the given span of characters, preserving
    /// the order of first occurrence.
    /// </summary>
    /// <param name="input">The input characters.</param>
    /// <returns>A new string with duplicate characters removed.</returns>
    public static string RemoveDuplicateCharacters(this ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            return string.Empty;
        }

        char[]? pooledBuffer = null;
        Span<char> buffer = (uint)input.Length <= StackAllocThreshold
            ? stackalloc char[input.Length]
            : (pooledBuffer = ArrayPool<char>.Shared.Rent(input.Length));

        try
        {
            var seen = new HashSet<char>(input.Length);
            var count = 0;

            foreach (var c in input)
            {
                if (seen.Add(c))
                {
                    buffer[count++] = c;
                }
            }

            return new string(buffer[..count]);
        }
        finally
        {
            if (pooledBuffer is not null)
            {
                ArrayPool<char>.Shared.Return(pooledBuffer);
            }
        }
    }

    /// <summary>
    /// Converts the given string to camel case.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The input string converted to camel case.</returns>
    public static string ToCamelCase(this string input)
    {
        return ToCamelCase(input.AsSpan());
    }

    /// <summary>
    /// Converts the given span of characters to camel case.
    /// </summary>
    /// <param name="input">The input characters.</param>
    /// <returns>The input converted to camel case.</returns>
    public static string ToCamelCase(this ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            return string.Empty;
        }

        char[]? pooledBuffer = null;
        Span<char> buffer = (uint)input.Length <= StackAllocThreshold
            ? stackalloc char[input.Length]
            : (pooledBuffer = ArrayPool<char>.Shared.Rent(input.Length));

        try
        {
            var count = 0;
            var shouldCapitalize = false;

            foreach (var c in input)
            {
                if (char.IsWhiteSpace(c) || c == '_')
                {
                    shouldCapitalize = true;
                    continue;
                }

                buffer[count++] = shouldCapitalize ? char.ToUpperInvariant(c) : char.ToLowerInvariant(c);
                shouldCapitalize = false;
            }

            // The very first character is always lowercase, even if the input
            // started with a separator (e.g. "_hello" gives "hello", not "Hello").
            if (count > 0)
            {
                buffer[0] = char.ToLowerInvariant(buffer[0]);
            }

            return new string(buffer[..count]);
        }
        finally
        {
            if (pooledBuffer is not null)
            {
                ArrayPool<char>.Shared.Return(pooledBuffer);
            }
        }
    }
}