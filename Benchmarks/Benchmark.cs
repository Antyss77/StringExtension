using Benchmark;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using StringExtension;

BenchmarkRunner.Run<StringExtensionBenchmark>(
    ManualConfig.Create(DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator)));


namespace Benchmark {
    [MemoryDiagnoser(false)]
    public class StringExtensionBenchmark {
        private readonly string input = "hello world!";
        private readonly char[] charactersToRemove = {'l', 'o'};
        private readonly string oldValue = "world";
        private readonly string newValue = "everyone";

        private readonly string substring = "l";

        private readonly string phoneNumber = "555-555-5555";
        private readonly string email = "john.doe@example.com";

        [Benchmark]
        public string RemoveCharacters() {
            return input.RemoveCharacters(charactersToRemove);
        }

        [Benchmark]
        public bool IsValidEmail() {
            return email.IsValidEmail();
        }

        [Benchmark]
        public bool IsValidPhoneNumber() {
            string phoneNumber = "555-555-5555";
            return phoneNumber.IsValidPhoneNumber();
        }

        [Benchmark]
        public int CountSubstring() {
            return input.CountSubstring(substring);
        }

        [Benchmark]
        public string ReverseWords() {
            return input.ReverseWords();
        }

        [Benchmark]
        public bool IsPalindrome() {
            string palindrome = "racecar";
            return palindrome.IsPalindrome();
        }

        [Benchmark]
        public int CountLetters() {
            return input.CountLetters();
        }

        [Benchmark]
        public string RemoveDuplicateCharacters() {
            return input.RemoveDuplicateCharacters();
        }

        [Benchmark]
        public string ConvertToCamelCase() {
            string text = "hello_world";
            return text.ToCamelCase();
        }
    }
}