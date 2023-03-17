﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Strings;
using StringsPerf;

BenchmarkRunner.Run<StringsBenchmark>(
    ManualConfig.Create(DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator)));


namespace StringsPerf {
    [MemoryDiagnoser(false)]
    public class StringsBenchmark {
        private readonly string input = "hello world!";
        private readonly char[] charactersToRemove = {'l', 'o'};
        private readonly string oldValue = "world";
        private readonly string newValue = "everyone";

        private readonly string substring = "l";

        [Benchmark]
        public string RemoveCharacters() {
            return input.RemoveCharacters(charactersToRemove);
        }


         [Benchmark]
         public bool IsValidEmail() {
             string email = "john.doe@example.com";
             return email.IsValidEmail();
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
             string input = "hello_world";
             return input.ConvertToCamelCase();
         }
    }
}