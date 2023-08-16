using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
using StringExtension;

namespace UnitTests;

public class Tests {
    [SetUp]
    public void Setup() {
    }
    
    public class UnitTest {
        [Test]
        public void TestRemoveCharacters() {
            string input = "hello world!";
            char[] charactersToRemove = {'l', 'o'};
            string expected = "he wrd!";
            string result = input.RemoveCharacters(charactersToRemove);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestIsValidEmail() {
            string email = "john.doe@example.com";
            bool result = email.IsValidEmail();
            Assert.IsTrue(result);
        }


        [Test]
        public void TestIsValidPhoneNumber() {
            string phoneNumber = "555-555-5555";
            bool result = phoneNumber.IsValidPhoneNumber();
            Assert.IsTrue(result);
        }

        [Test]
        public void TestCountSubstring() {
            string input = "hello world!";
            string substring = "l";
            int expected = 3;
            int result = input.CountSubstring(substring);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestReverseWords() {
            string input = "hello world!";
            string expected = "world! hello";
            string result = input.ReverseWords();
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestIsPalindrome() {
            string input = "racecar";
            bool result = input.IsPalindrome();
            Assert.IsTrue(result);
        }

        [Test]
        public void TestCountLetters() {
            string input = "hello world!";
            int expected = 10;
            int result = input.CountLetters();
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestRemoveDuplicateCharacters() {
            string input = "hello world!";
            string expected = "helo wrd!";
            string result = input.RemoveDuplicateCharacters();
            Assert.That(result, Is.EqualTo(expected));
        }


        [Test]
        public void TestConvertToCamelCase() {
            string input = "hello_world";
            string expected = "helloWorld";
            string result = input.ToCamelCase();
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}