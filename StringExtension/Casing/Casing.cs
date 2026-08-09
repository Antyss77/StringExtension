using System.Buffers;
using System.Globalization;
using System.Text;
using StringExtension.Internal;

namespace StringExtension.Casing;

/// <summary>
/// Provides extension methods for converting the casing of strings.
/// </summary>
public static class Casing
{
    /// <summary>
    /// English words that are conventionally left in lowercase in title case,
    /// unless they are the first or last word of the input.
    /// </summary>
    private static readonly string[] EnglishMinorWords =
    {
        "a", "an", "and", "as", "at", "but", "by", "for", "from", "if", "in",
        "into", "nor", "of", "on", "onto", "or", "over", "the", "to", "with",
    };

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
    /// Whitespace, <c>_</c>, and <c>-</c> are treated as word separators. Casing
    /// transitions already present in the input (e.g. the "W" in "helloWorld")
    /// are not treated as word boundaries.
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
                if (IsWordSeparator(rune))
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

    /// <summary>
    /// Converts the given string to Pascal case.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The input string converted to Pascal case.</returns>
    public static string ToPascalCase(this string input)
    {
        return ToPascalCase(input.AsSpan());
    }

    /// <summary>
    /// Converts the given span of characters to Pascal case.
    /// </summary>
    /// <param name="input">The input characters.</param>
    /// <returns>The input converted to Pascal case.</returns>
    /// <remarks>
    /// Whitespace, <c>_</c>, and <c>-</c> are treated as word separators. Casing
    /// transitions already present in the input are not treated as word boundaries.
    /// </remarks>
    public static string ToPascalCase(this ReadOnlySpan<char> input)
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
            var shouldCapitalize = true;

            foreach (var rune in input.EnumerateRunes())
            {
                if (IsWordSeparator(rune))
                {
                    shouldCapitalize = true;
                    continue;
                }

                var cased = shouldCapitalize ? Rune.ToUpperInvariant(rune) : Rune.ToLowerInvariant(rune);
                count += cased.EncodeToUtf16(buffer[count..]);
                shouldCapitalize = false;
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
    /// Converts the given string to snake case.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The input string converted to snake case.</returns>
    public static string ToSnakeCase(this string input)
    {
        return ToSnakeCase(input.AsSpan());
    }

    /// <summary>
    /// Converts the given span of characters to snake case.
    /// </summary>
    /// <param name="input">The input characters.</param>
    /// <returns>The input converted to snake case.</returns>
    /// <remarks>
    /// A word boundary is inserted at whitespace, <c>_</c>, <c>-</c>, and at any
    /// transition from a lowercase letter or digit to an uppercase letter (e.g.
    /// "helloWorld" gives "hello_world"). Consecutive uppercase letters (as in an
    /// acronym, e.g. "HTTPServer") are treated as a single word rather than being
    /// split individually.
    /// </remarks>
    public static string ToSnakeCase(this ReadOnlySpan<char> input)
    {
        return ToSeparatedLowerCase(input, '_');
    }

    /// <summary>
    /// Converts the given string to kebab case.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The input string converted to kebab case.</returns>
    public static string ToKebabCase(this string input)
    {
        return ToKebabCase(input.AsSpan());
    }

    /// <summary>
    /// Converts the given span of characters to kebab case.
    /// </summary>
    /// <param name="input">The input characters.</param>
    /// <returns>The input converted to kebab case.</returns>
    /// <remarks>
    /// A word boundary is inserted at whitespace, <c>_</c>, <c>-</c>, and at any
    /// transition from a lowercase letter or digit to an uppercase letter (e.g.
    /// "helloWorld" gives "hello-world"). Consecutive uppercase letters (as in an
    /// acronym, e.g. "HTTPServer") are treated as a single word rather than being
    /// split individually.
    /// </remarks>
    public static string ToKebabCase(this ReadOnlySpan<char> input)
    {
        return ToSeparatedLowerCase(input, '-');
    }

    /// <summary>
    /// Converts the given string to title case.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="useEnglishMinorWordRules">
    /// If <see langword="true"/>, common short English words (e.g. "of", "the",
    /// "and") are left in lowercase unless they are the first or last word.
    /// If <see langword="false"/> (the default), every word is capitalized.
    /// </param>
    /// <returns>The input string converted to title case.</returns>
    public static string ToTitleCase(this string input, bool useEnglishMinorWordRules = false)
    {
        return ToTitleCase(input.AsSpan(), useEnglishMinorWordRules);
    }

    /// <summary>
    /// Converts the given span of characters to title case.
    /// </summary>
    /// <param name="input">The input characters.</param>
    /// <param name="useEnglishMinorWordRules">
    /// If <see langword="true"/>, common short English words (e.g. "of", "the",
    /// "and") are left in lowercase unless they are the first or last word.
    /// If <see langword="false"/> (the default), every word is capitalized.
    /// </param>
    /// <returns>The input converted to title case.</returns>
    /// <remarks>
    /// Words are assumed to be separated by single spaces. <paramref name="useEnglishMinorWordRules"/>
    /// applies a fixed, English-specific list of minor words and is not suitable
    /// for other languages.
    /// </remarks>
    public static string ToTitleCase(this ReadOnlySpan<char> input, bool useEnglishMinorWordRules = false)
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
            var isFirstWord = true;
            var lastWordStart = 0;

            foreach (Range wordRange in input.Split(' '))
            {
                ReadOnlySpan<char> word = input[wordRange];

                if (word.IsEmpty)
                {
                    continue;
                }

                if (count > 0)
                {
                    buffer[count++] = ' ';
                }

                lastWordStart = count;

                var keepLowercase = useEnglishMinorWordRules && !isFirstWord && IsEnglishMinorWord(word);
                var isFirstRuneOfWord = true;

                foreach (var rune in word.EnumerateRunes())
                {
                    var cased = isFirstRuneOfWord && !keepLowercase
                        ? Rune.ToUpperInvariant(rune)
                        : Rune.ToLowerInvariant(rune);

                    count += cased.EncodeToUtf16(buffer[count..]);
                    isFirstRuneOfWord = false;
                }

                isFirstWord = false;
            }

            // The last word is always capitalized regardless of the minor-word
            // list, matching standard title case conventions (e.g. "... of the
            // Rings", not "... of the rings").
            if (useEnglishMinorWordRules && lastWordStart < count)
            {
                Rune.DecodeFromUtf16(buffer[lastWordStart..count], out var firstRuneOfLastWord, out _);
                Rune.ToUpperInvariant(firstRuneOfLastWord).EncodeToUtf16(buffer[lastWordStart..count]);
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
    /// Converts the given span of characters to lowercase, inserting <paramref name="separator"/>
    /// at whitespace, <c>_</c>, <c>-</c>, and at any transition from a lowercase
    /// letter or digit to an uppercase letter.
    /// </summary>
    private static string ToSeparatedLowerCase(ReadOnlySpan<char> input, char separator)
    {
        if (input.IsEmpty)
        {
            return string.Empty;
        }

        // Worst case, a separator is inserted before nearly every character
        // (e.g. alternating case input), so the output can be up to twice as long
        // as the input.
        var maxLength = input.Length * 2;

        char[]? pooledBuffer = null;
        Span<char> buffer = (uint)maxLength <= BufferLimits.StackAllocThreshold
            ? stackalloc char[maxLength]
            : (pooledBuffer = ArrayPool<char>.Shared.Rent(maxLength));

        try
        {
            var count = 0;
            var atWordStart = true;
            var previousWasLowerOrDigit = false;

            foreach (var rune in input.EnumerateRunes())
            {
                if (IsWordSeparator(rune))
                {
                    if (count > 0)
                    {
                        atWordStart = true;
                    }

                    previousWasLowerOrDigit = false;
                    continue;
                }

                var isUpper = Rune.IsUpper(rune);
                var isNewWord = atWordStart
                    ? count > 0
                    : isUpper && previousWasLowerOrDigit;

                if (isNewWord)
                {
                    buffer[count++] = separator;
                }

                count += Rune.ToLowerInvariant(rune).EncodeToUtf16(buffer[count..]);
                previousWasLowerOrDigit = !isUpper;
                atWordStart = false;
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

    private static bool IsWordSeparator(Rune rune)
    {
        return Rune.IsWhiteSpace(rune) || rune.Value == '_' || rune.Value == '-';
    }

    private static bool IsEnglishMinorWord(ReadOnlySpan<char> word)
    {
        foreach (var minorWord in EnglishMinorWords)
        {
            if (word.Equals(minorWord, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Converts the given string into a URL-friendly slug: lowercase, with accents
    /// removed and every run of non-alphanumeric characters collapsed into a single
    /// <paramref name="separator"/>.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="separator">The character used to join words. Defaults to <c>-</c>.</param>
    /// <returns>The slugified string.</returns>
    /// <remarks>
    /// Accented Latin letters are converted to their unaccented equivalent (e.g.
    /// "é" becomes "e") via Unicode decomposition. Letters from non-Latin scripts
    /// (e.g. Cyrillic, CJK) are lowercased and kept as-is rather than being
    /// stripped or transliterated. Returns <see cref="string.Empty"/> if
    /// <paramref name="input"/> is <see langword="null"/> or empty.
    /// </remarks>
    public static string Slugify(this string input, char separator = '-')
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        ReadOnlySpan<char> normalized = input.Normalize(NormalizationForm.FormD);

        char[]? pooledBuffer = null;
        Span<char> buffer = (uint)normalized.Length <= BufferLimits.StackAllocThreshold
            ? stackalloc char[normalized.Length]
            : (pooledBuffer = ArrayPool<char>.Shared.Rent(normalized.Length));

        try
        {
            var count = 0;
            var pendingSeparator = false;

            foreach (var rune in normalized.EnumerateRunes())
            {
                if (Rune.GetUnicodeCategory(rune) == UnicodeCategory.NonSpacingMark)
                {
                    // A combining accent mark produced by decomposition (e.g. the
                    // acute accent split off "é"). The base letter it modifies was
                    // already appended; drop the mark itself.
                    continue;
                }

                if (Rune.IsLetterOrDigit(rune))
                {
                    if (pendingSeparator && count > 0)
                    {
                        buffer[count++] = separator;
                    }

                    count += Rune.ToLowerInvariant(rune).EncodeToUtf16(buffer[count..]);
                    pendingSeparator = false;
                }
                else
                {
                    pendingSeparator = true;
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