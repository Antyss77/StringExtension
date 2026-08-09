using StringExtension;
using StringExtension.Casing;
using StringExtension.Linguistics;
using StringExtension.Validation;

namespace UnitTests;

/// <summary>
/// Contains unit tests for the StringExtension methods.
/// </summary>
public class StringExtensionTests
{
    /// <summary>
    /// Tests the RemoveCharacters method.
    /// </summary>
    [Test]
    public void TestRemoveCharacters()
    {
        string input = "hello world!";
        char[] charactersToRemove = { 'l', 'o' };
        string expected = "he wrd!";
        string result = input.RemoveCharacters(charactersToRemove);
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests that RemoveCharacters handles a null input gracefully.
    /// </summary>
    [Test]
    public void TestRemoveCharacters_NullInput()
    {
        string input = null!;
        char[] charactersToRemove = { 'l', 'o' };
        string result = input.RemoveCharacters(charactersToRemove);
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests the ReadOnlySpan&lt;char&gt; overload of RemoveCharacters.
    /// </summary>
    [Test]
    public void TestRemoveCharacters_Span()
    {
        ReadOnlySpan<char> input = "hello world!";
        ReadOnlySpan<char> charactersToRemove = "lo";
        string expected = "he wrd!";
        string result = input.RemoveCharacters(charactersToRemove);
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests the IsValidEmail method.
    /// </summary>
    [Test]
    public void TestIsValidEmail()
    {
        string email = "john.doe@example.com";
        bool result = email.IsValidEmail();
        Assert.That(result, Is.True);
    }

    /// <summary>
    /// Tests the IsValidPhoneNumber method.
    /// </summary>
    [Test]
    public void TestIsValidPhoneNumber()
    {
        string phoneNumber = "555-555-5555";
        bool result = phoneNumber.IsValidPhoneNumber();
        Assert.That(result, Is.True);
    }

    /// <summary>
    /// Tests the CountSubstring method.
    /// </summary>
    [Test]
    public void TestCountSubstring()
    {
        string input = "hello world!";
        string substring = "l";
        int expected = 3;
        int result = input.CountSubstring(substring);
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests the ReverseWords method.
    /// </summary>
    [Test]
    public void TestReverseWords()
    {
        string input = "hello world!";
        string expected = "world! hello";
        string result = input.ReverseWords();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests that ReverseWords handles a null input gracefully.
    /// </summary>
    [Test]
    public void TestReverseWords_NullInput()
    {
        string input = null!;
        string result = input.ReverseWords();
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests the IsPalindrome method.
    /// </summary>
    [Test]
    public void TestIsPalindrome()
    {
        string input = "racecar";
        bool result = input.IsPalindrome();
        Assert.That(result, Is.True);
    }

    /// <summary>
    /// Tests that IsPalindrome handles a null input gracefully.
    /// </summary>
    [Test]
    public void TestIsPalindrome_NullInput()
    {
        string input = null!;
        bool result = input.IsPalindrome();
        Assert.That(result, Is.False);
    }

    /// <summary>
    /// Tests that IsPalindrome correctly compares characters outside the Basic
    /// Multilingual Plane (encoded as UTF-16 surrogate pairs) as whole units.
    /// "\U0001D49C" and "\U0001D4B7" are two different mathematical script
    /// letters; treating each surrogate half as a separate "non-letter" character
    /// (the pre-Rune behavior) would have skipped them entirely and produced a
    /// false positive.
    /// </summary>
    [Test]
    public void TestIsPalindrome_SurrogatePairLetters()
    {
        string input = "\U0001D49Cb\U0001D4B7";
        bool result = input.IsPalindrome();
        Assert.That(result, Is.False);
    }

    /// <summary>
    /// Tests the CountLetters method.
    /// </summary>
    [Test]
    public void TestCountLetters()
    {
        string input = "hello world!";
        int expected = 10;
        int result = input.CountLetters();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests that CountLetters handles a null input gracefully.
    /// </summary>
    [Test]
    public void TestCountLetters_NullInput()
    {
        string input = null!;
        int result = input.CountLetters();
        Assert.That(result, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that CountLetters correctly counts a letter outside the Basic
    /// Multilingual Plane. "\U0001D49C" (MATHEMATICAL SCRIPT CAPITAL A) is
    /// encoded as a UTF-16 surrogate pair; the pre-Rune implementation counted
    /// it as 0 letters, since neither surrogate half is classified as a letter
    /// on its own.
    /// </summary>
    [Test]
    public void TestCountLetters_SurrogatePairLetter()
    {
        string input = "\U0001D49C";
        int result = input.CountLetters();
        Assert.That(result, Is.EqualTo(1));
    }

    /// <summary>
    /// Tests the RemoveDuplicateCharacters method.
    /// </summary>
    [Test]
    public void TestRemoveDuplicateCharacters()
    {
        string input = "hello world!";
        string expected = "helo wrd!";
        string result = input.RemoveDuplicateCharacters();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests the ToCamelCase method.
    /// </summary>
    [Test]
    public void TestConvertToCamelCase()
    {
        string input = "hello_world";
        string expected = "helloWorld";
        string result = input.ToCamelCase();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests that ToCamelCase correctly lowercases the first character
    /// even when the input starts with a separator.
    /// </summary>
    [Test]
    public void TestConvertToCamelCase_LeadingSeparator()
    {
        string input = "_hello_world";
        string expected = "helloWorld";
        string result = input.ToCamelCase();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests that ToCamelCase preserves a character outside the Basic Multilingual
    /// Plane (a surrogate pair) intact, instead of splitting or corrupting it.
    /// "\U0001F389" is the party popper emoji (🎉).
    /// </summary>
    [Test]
    public void TestConvertToCamelCase_SurrogatePair()
    {
        string input = "hello_\U0001F389_world";
        string expected = "hello\U0001F389World";
        string result = input.ToCamelCase();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests that ToCamelCase treats a hyphen as a word separator.
    /// </summary>
    [Test]
    public void TestConvertToCamelCase_HyphenSeparator()
    {
        string input = "hello-world";
        string expected = "helloWorld";
        string result = input.ToCamelCase();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests the ToPascalCase method.
    /// </summary>
    [Test]
    public void TestToPascalCase()
    {
        string input = "hello_world";
        string expected = "HelloWorld";
        string result = input.ToPascalCase();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests the ToSnakeCase method with camelCase input.
    /// </summary>
    [Test]
    public void TestToSnakeCase_CamelCaseInput()
    {
        string input = "helloWorld";
        string expected = "hello_world";
        string result = input.ToSnakeCase();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests the ToSnakeCase method with space-separated input.
    /// </summary>
    [Test]
    public void TestToSnakeCase_SpaceSeparatedInput()
    {
        string input = "hello world";
        string expected = "hello_world";
        string result = input.ToSnakeCase();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests the ToKebabCase method with camelCase input.
    /// </summary>
    [Test]
    public void TestToKebabCase_CamelCaseInput()
    {
        string input = "helloWorld";
        string expected = "hello-world";
        string result = input.ToKebabCase();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests the ToTitleCase method with the default behavior (every word capitalized).
    /// </summary>
    [Test]
    public void TestToTitleCase_Default()
    {
        string input = "the lord of the rings";
        string expected = "The Lord Of The Rings";
        string result = input.ToTitleCase();
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests the ToTitleCase method with English minor-word rules enabled.
    /// </summary>
    [Test]
    public void TestToTitleCase_EnglishMinorWordRules()
    {
        string input = "the lord of the rings";
        string expected = "The Lord of the Rings";
        string result = input.ToTitleCase(useEnglishMinorWordRules: true);
        Assert.That(result, Is.EqualTo(expected));
    }

    /// <summary>
    /// Tests that ToTitleCase always capitalizes the last word, even if it is
    /// normally a minor word.
    /// </summary>
    [Test]
    public void TestToTitleCase_LastWordAlwaysCapitalized()
    {
        string input = "what are you waiting for";
        string expected = "What Are You Waiting For";
        string result = input.ToTitleCase(useEnglishMinorWordRules: true);
        Assert.That(result, Is.EqualTo(expected));
    }
}