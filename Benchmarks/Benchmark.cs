using Benchmark;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using StringExtension;

BenchmarkRunner.Run<StringExtensionBenchmark>(
    ManualConfig.Create(DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator)));


namespace Benchmark {

    /// <summary>
    /// Contains benchmarks for the StringExtension methods.
    /// </summary>
    [MemoryDiagnoser(false)]
    public class StringExtensionBenchmark {

        private readonly string input = "hello world!";
        private readonly char[] charactersToRemove = { 'l', 'o' };
        private readonly string substring = "l";
        private readonly string phoneNumber = "555-555-5555";
        private readonly string email = "john.doe@example.com";

        /// <summary>
        /// Benchmark for the RemoveCharacters method.
        /// </summary>
        [Benchmark]
        public string RemoveCharacters() {
            return input.RemoveCharacters(charactersToRemove);
        }

        /// <summary>
        /// Benchmark for the IsValidEmail method.
        /// </summary>
        [Benchmark]
        public bool IsValidEmail() {
            return email.IsValidEmail();
        }

        /// <summary>
        /// Benchmark for the IsValidPhoneNumber method.
        /// </summary>
        [Benchmark]
        public bool IsValidPhoneNumber() {
            return phoneNumber.IsValidPhoneNumber();
        }

        /// <summary>
        /// Benchmark for the CountSubstring method.
        /// </summary>
        [Benchmark]
        public int CountSubstring() {
            return input.CountSubstring(substring);
        }

        /// <summary>
        /// Benchmark for the ReverseWords method.
        /// </summary>
        [Benchmark]
        public string ReverseWords() {
            return input.ReverseWords();
        }

        /// <summary>
        /// Benchmark for the IsPalindrome method.
        /// </summary>
        [Benchmark]
        public bool IsPalindrome() {
            return input.IsPalindrome();
        }

        /// <summary>
        /// Benchmark for the CountLetters method.
        /// </summary>
        [Benchmark]
        public int CountLetters() {
            return input.CountLetters();
        }

        /// <summary>
        /// Benchmark for the RemoveDuplicateCharacters method.
        /// </summary>
        [Benchmark]
        public string RemoveDuplicateCharacters() {
            return input.RemoveDuplicateCharacters();
        }

        /// <summary>
        /// Benchmark for the ConvertToCamelCase method.
        /// </summary>
        [Benchmark]
        public string ConvertToCamelCase() {
            return input.ToCamelCase();
        }
    }
}
