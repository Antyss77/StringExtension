using StringExtension;

namespace UnitTests;

/// <summary>
/// Contains unit tests for the StringExtension methods.
/// </summary>
public class Tests {
    [SetUp]
    public void Setup() {
    }

    public class UnitTest {
        /// <summary>
        /// Tests the RemoveCharacters method.
        /// </summary>
        [Test]
        public void TestRemoveCharacters() {
            string input = "hello world!";
            char[] charactersToRemove = {'l', 'o'};
            string expected = "he wrd!";
            string result = input.RemoveCharacters(charactersToRemove);
            Assert.That(result, Is.EqualTo(expected));
            // Test code and assertions explained in code.
        }

        /// <summary>
        /// Tests that RemoveCharacters handles a null input gracefully.
        /// </summary>
        [Test]
        public void TestRemoveCharacters_NullInput() {
            string input = null;
            char[] charactersToRemove = {'l', 'o'};
            string result = input.RemoveCharacters(charactersToRemove);
            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// Tests the IsValidEmail method.
        /// </summary>
        [Test]
        public void TestIsValidEmail() {
            string email = "john.doe@example.com";
            bool result = email.IsValidEmail();
            Assert.IsTrue(result);
            // Test code and assertions explained in code.
        }

        /// <summary>
        /// Tests the IsValidPhoneNumber method.
        /// </summary>
        [Test]
        public void TestIsValidPhoneNumber() {
            string phoneNumber = "555-555-5555";
            bool result = phoneNumber.IsValidPhoneNumber();
            Assert.IsTrue(result);
            // Test code and assertions explained in code.
        }

        /// <summary>
        /// Tests the CountSubstring method.
        /// </summary>
        [Test]
        public void TestCountSubstring() {
            string input = "hello world!";
            string substring = "l";
            int expected = 3;
            int result = input.CountSubstring(substring);
            Assert.That(result, Is.EqualTo(expected));
            // Test code and assertions explained in code.
        }

        /// <summary>
        /// Tests the ReverseWords method.
        /// </summary>
        [Test]
        public void TestReverseWords() {
            string input = "hello world!";
            string expected = "world! hello";
            string result = input.ReverseWords();
            Assert.That(result, Is.EqualTo(expected));
            // Test code and assertions explained in code.
        }

        /// <summary>
        /// Tests that ReverseWords handles a null input gracefully.
        /// </summary>
        [Test]
        public void TestReverseWords_NullInput() {
            string input = null;
            string result = input.ReverseWords();
            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// Tests the IsPalindrome method.
        /// </summary>
        [Test]
        public void TestIsPalindrome() {
            string input = "racecar";
            bool result = input.IsPalindrome();
            Assert.IsTrue(result);
            // Test code and assertions explained in code.
        }

        /// <summary>
        /// Tests that IsPalindrome handles a null input gracefully.
        /// </summary>
        [Test]
        public void TestIsPalindrome_NullInput() {
            string input = null;
            bool result = input.IsPalindrome();
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the CountLetters method.
        /// </summary>
        [Test]
        public void TestCountLetters() {
            string input = "hello world!";
            int expected = 10;
            int result = input.CountLetters();
            Assert.That(result, Is.EqualTo(expected));
            // Test code and assertions explained in code.
        }

        /// <summary>
        /// Tests that CountLetters handles a null input gracefully.
        /// </summary>
        [Test]
        public void TestCountLetters_NullInput() {
            string input = null;
            int result = input.CountLetters();
            Assert.That(result, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests the RemoveDuplicateCharacters method.
        /// </summary>
        [Test]
        public void TestRemoveDuplicateCharacters() {
            string input = "hello world!";
            string expected = "helo wrd!";
            string result = input.RemoveDuplicateCharacters();
            Assert.That(result, Is.EqualTo(expected));
            // Test code and assertions explained in code.
        }

        /// <summary>
        /// Tests the ToCamelCase method.
        /// </summary>
        [Test]
        public void TestConvertToCamelCase() {
            string input = "hello_world";
            string expected = "helloWorld";
            string result = input.ToCamelCase();
            Assert.That(result, Is.EqualTo(expected));
            // Test code and assertions explained in code.
        }

        /// <summary>
        /// Tests that ToCamelCase correctly lowercases the first character
        /// even when the input starts with a separator.
        /// </summary>
        [Test]
        public void TestConvertToCamelCase_LeadingSeparator() {
            string input = "_hello_world";
            string expected = "helloWorld";
            string result = input.ToCamelCase();
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}