using System.Buffers;
using System.Text;
using StringExtension.Internal;

namespace StringExtension.Linguistics;

/// <summary>
/// Provides extension methods for linguistic analysis of strings.
/// </summary>
public static class Linguistics
{
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
    /// </summary>
    /// <param name="input">The input characters.</param>
    /// <returns><c>true</c> if the characters form a palindrome; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Compares whole Unicode scalar values (<see cref="Rune"/>) rather than UTF-16
    /// code units, so multi-char characters (e.g. characters outside the Basic
    /// Multilingual Plane, such as many emoji) are compared correctly instead of
    /// being split into unpaired surrogate halves.
    /// </remarks>
    public static bool IsPalindrome(this ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            return false;
        }

        Rune[]? pooledBuffer = null;
        Span<Rune> letters = input.Length <= BufferLimits.StackAllocThreshold
            ? stackalloc Rune[input.Length]
            : (pooledBuffer = ArrayPool<Rune>.Shared.Rent(input.Length));

        try
        {
            var count = 0;
            foreach (var rune in input.EnumerateRunes())
            {
                if (Rune.IsLetter(rune))
                {
                    letters[count++] = rune;
                }
            }

            var left = 0;
            var right = count - 1;

            while (left < right)
            {
                if (Rune.ToLowerInvariant(letters[left]) != Rune.ToLowerInvariant(letters[right]))
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }
        finally
        {
            if (pooledBuffer is not null)
            {
                ArrayPool<Rune>.Shared.Return(pooledBuffer);
            }
        }
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
    /// <remarks>
    /// Classifies whole Unicode scalar values (<see cref="Rune"/>) rather than UTF-16
    /// code units, so letters outside the Basic Multilingual Plane are counted
    /// correctly instead of being missed as unpaired surrogate halves.
    /// </remarks>
    public static int CountLetters(this ReadOnlySpan<char> input)
    {
        var count = 0;
        foreach (var rune in input.EnumerateRunes())
        {
            if (Rune.IsLetter(rune))
            {
                count++;
            }
        }

        return count;
    }
}