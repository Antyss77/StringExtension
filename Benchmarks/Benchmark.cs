using System.Buffers;
using Benchmark;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using StringExtension;
using StringExtension.Casing;
using StringExtension.Linguistics;
using StringExtension.Validation;

var config = ManualConfig.Create(DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator));

if (args.Length == 0)
{
    BenchmarkSwitcher.FromAssembly(typeof(StringExtensionBenchmark).Assembly).Run(new[] { "--filter", "*" }, config);
}
else
{
    BenchmarkSwitcher.FromAssembly(typeof(StringExtensionBenchmark).Assembly).Run(args, config);
}

namespace Benchmark
{
    /// <summary>
    /// Contains benchmarks for the StringExtension methods.
    /// </summary>
    [MemoryDiagnoser(false)]
    public class StringExtensionBenchmark
    {
        private readonly string input = "hello world!";
        private readonly string longInput = string.Concat(Enumerable.Repeat("hello world! ", 100));
        private readonly char[] charactersToRemove = { 'l', 'o' };
        private readonly SearchValues<char> searchValues = SearchValues.Create('l', 'o');
        private readonly string substring = "l";
        private readonly string phoneNumber = "555-555-5555";
        private readonly string email = "john.doe@example.com";

        /// <summary>
        /// Benchmark for the RemoveCharacters method with char[].
        /// </summary>
        [Benchmark]
        public string? RemoveCharacters_CharArray()
        {
            return input.RemoveCharacters(charactersToRemove);
        }

        /// <summary>
        /// Benchmark for the RemoveCharacters method with SearchValues.
        /// </summary>
        [Benchmark]
        public string? RemoveCharacters_SearchValues()
        {
            return input.RemoveCharacters(searchValues);
        }

        /// <summary>
        /// Benchmark for the RemoveCharacters method on long input with char[].
        /// </summary>
        [Benchmark]
        public string? RemoveCharacters_Long_CharArray()
        {
            return longInput.RemoveCharacters(charactersToRemove);
        }

        /// <summary>
        /// Benchmark for the RemoveCharacters method on long input with SearchValues.
        /// </summary>
        [Benchmark]
        public string? RemoveCharacters_Long_SearchValues()
        {
            return longInput.RemoveCharacters(searchValues);
        }

        /// <summary>
        /// Benchmark for the IsValidEmail method.
        /// </summary>
        [Benchmark]
        public bool IsValidEmail()
        {
            return email.IsValidEmail();
        }

        /// <summary>
        /// Benchmark for the IsValidPhoneNumber method.
        /// </summary>
        [Benchmark]
        public bool IsValidPhoneNumber()
        {
            return phoneNumber.IsValidPhoneNumber();
        }

        /// <summary>
        /// Benchmark for the CountSubstring method.
        /// </summary>
        [Benchmark]
        public int CountSubstring()
        {
            return input.CountSubstring(substring);
        }

        /// <summary>
        /// Benchmark for the ReverseWords method.
        /// </summary>
        [Benchmark]
        public string? ReverseWords()
        {
            return input.ReverseWords();
        }

        /// <summary>
        /// Benchmark for the IsPalindrome method.
        /// </summary>
        [Benchmark]
        public bool IsPalindrome()
        {
            return input.IsPalindrome();
        }

        /// <summary>
        /// Benchmark for the CountLetters method.
        /// </summary>
        [Benchmark]
        public int CountLetters()
        {
            return input.CountLetters();
        }

        /// <summary>
        /// Benchmark for the RemoveDuplicateCharacters method.
        /// </summary>
        [Benchmark]
        public string? RemoveDuplicateCharacters()
        {
            return input.RemoveDuplicateCharacters();
        }

        /// <summary>
        /// Benchmark for the ConvertToCamelCase method.
        /// </summary>
        [Benchmark]
        public string ConvertToCamelCase()
        {
            return input.ToCamelCase();
        }

        /// <summary>
        /// Benchmark for the ToPascalCase method.
        /// </summary>
        [Benchmark]
        public string ToPascalCase()
        {
            return input.ToPascalCase();
        }

        /// <summary>
        /// Benchmark for the ToSnakeCase method.
        /// </summary>
        [Benchmark]
        public string ToSnakeCase()
        {
            return input.ToSnakeCase();
        }

        /// <summary>
        /// Benchmark for the ToKebabCase method.
        /// </summary>
        [Benchmark]
        public string ToKebabCase()
        {
            return input.ToKebabCase();
        }

        /// <summary>
        /// Benchmark for the ToTitleCase method.
        /// </summary>
        [Benchmark]
        public string ToTitleCase()
        {
            return input.ToTitleCase();
        }

        /// <summary>
        /// Benchmark for the Slugify method.
        /// </summary>
        [Benchmark]
        public string Slugify()
        {
            return input.Slugify();
        }
    }
}