using System.Buffers;
using System.Text;
using StringExtension.Internal;

namespace StringExtension.Casing;

/// <summary>
/// Provides extension methods for converting the casing of strings.
/// </summary>
public static class Casing
{
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
    /// <remarks>
    /// Cases whole Unicode scalar values (<see cref="Rune"/>) rather than UTF-16
    /// code units, so characters outside the Basic Multilingual Plane are handled
    /// correctly instead of having casing applied to unpaired surrogate halves.
    /// </remarks>
    public static string ToCamelCase(this ReadOnlySpan<char> input)
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
            var count = 0;
            var shouldCapitalize = false;
            var isFirstWrittenRune = true;

            foreach (var rune in input.EnumerateRunes())
            {
                if (Rune.IsWhiteSpace(rune) || rune.Value == '_')
                {
                    shouldCapitalize = true;
                    continue;
                }

                // The very first character is always lowercase, even if the input
                // started with a separator (e.g. "_hello" gives "hello", not "Hello").
                var cased = isFirstWrittenRune || !shouldCapitalize
                    ? Rune.ToLowerInvariant(rune)
                    : Rune.ToUpperInvariant(rune);

                count += cased.EncodeToUtf16(buffer[count..]);
                shouldCapitalize = false;
                isFirstWrittenRune = false;
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