using System.Buffers;
using StringExtension.Internal;

namespace StringExtension;

/// <summary>
/// Provides general-purpose extension methods for string manipulation.
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Removes specified characters from the given string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="charactersToRemove">An array of characters to remove.</param>
    /// <returns>A new string with specified characters removed.</returns>
    /// <remarks>Returns <see langword="null"/> if <paramref name="input"/> is <see langword="null"/>.</remarks>
    public static string? RemoveCharacters(this string? input, char[]? charactersToRemove)
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
        Span<char> buffer = (uint)input.Length <= BufferLimits.StackAllocThreshold
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
    /// Counts the number of occurrences of a substring in the given string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="substring">The substring to count.</param>
    /// <returns>The number of occurrences of the substring in the input string.</returns>
    /// <remarks>Returns <c>0</c> if <paramref name="input"/> or <paramref name="substring"/> is <see langword="null"/>.</remarks>
    public static int CountSubstring(this string? input, string? substring)
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
    public static string? ReverseWords(this string? input)
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
    /// Removes duplicate characters from the given string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>A new string with duplicate characters removed.</returns>
    /// <remarks>Returns <see langword="null"/> if <paramref name="input"/> is <see langword="null"/>.</remarks>
    public static string? RemoveDuplicateCharacters(this string? input)
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
        Span<char> buffer = (uint)input.Length <= BufferLimits.StackAllocThreshold
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
}