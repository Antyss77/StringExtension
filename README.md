# StringExtension

[![NuGet Version](https://img.shields.io/nuget/v/String-Extension)](https://www.nuget.org/packages/String-Extension/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/String-Extension)](https://www.nuget.org/packages/String-Extension/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/Antyss77/StringExtension/blob/master/LICENSE)
[![.NET](https://github.com/Antyss77/StringExtension/actions/workflows/workflow.yml/badge.svg)](https://github.com/Antyss77/StringExtension/actions/workflows/workflow.yml)

A string manipulation library for C#, built around three principles: correctness, performance, and a minimal API surface.

- **Zero-allocation where possible.** Most methods operate on `Span<char>`/`ReadOnlySpan<char>` internally, using `stackalloc` for small inputs and `ArrayPool<char>` for larger ones instead of allocating a new array on every call.
- **Unicode-correct.** Methods that classify or compare characters operate on whole Unicode scalar values (via [`Rune`](https://learn.microsoft.com/en-us/dotnet/api/system.text.rune)) rather than raw UTF-16 code units, so characters outside the Basic Multilingual Plane (many emoji, some rare scripts) are handled correctly instead of being split into invalid half-characters.
- **Both `string` and `ReadOnlySpan<char>` overloads.** Every method that accepts a `string` also has a `ReadOnlySpan<char>` overload, so you can call it on a slice without allocating an intermediate string.

## Requirements

.NET 10.0 or later.

## Installation

```bash
dotnet add package String-Extension
```

## API

Methods are split across namespaces by category. Add the relevant `using` statement for the methods you need.

### `using StringExtension;`

General-purpose string manipulation.

| Method | Description |
| --- | --- |
| `RemoveCharacters(char[])` | Removes the specified characters from a string. |
| `CountSubstring(string)` | Counts the number of occurrences of a substring. |
| `ReverseWords()` | Reverses the order of words in a string. |
| `RemoveDuplicateCharacters()` | Removes duplicate characters, keeping the first occurrence of each. |

### `using StringExtension.Validation;`

Format validation.

| Method | Description |
| --- | --- |
| `IsValidEmail()` | Validates an e-mail address. |
| `IsValidPhoneNumber()` | Validates a phone number. |

### `using StringExtension.Linguistics;`

Text analysis.

| Method | Description |
| --- | --- |
| `IsPalindrome()` | Determines if a string is a [palindrome](https://en.wikipedia.org/wiki/Palindrome). |
| `CountLetters()` | Counts the number of letters in a string. |

### `using StringExtension.Casing;`

Casing conventions and slug generation.

| Method | Description |
| --- | --- |
| `ToCamelCase()` | Converts to camelCase. |
| `ToPascalCase()` | Converts to PascalCase. |
| `ToSnakeCase()` | Converts to snake_case. |
| `ToKebabCase()` | Converts to kebab-case. |
| `ToTitleCase(bool)` | Converts to Title Case, with an optional flag to keep short English words (of, the, and, ...) lowercase. |
| `Slugify(char)` | Converts to a URL-friendly slug, stripping accents and collapsing punctuation into a single separator. |

## Examples

```csharp
using StringExtension;
using StringExtension.Casing;
using StringExtension.Linguistics;
using StringExtension.Validation;

"hello world!".RemoveCharacters(['l', 'o']);   // "he wrd!"
"hello world!".ReverseWords();                 // "world! hello"

"john.doe@example.com".IsValidEmail();         // true
"racecar".IsPalindrome();                      // true

"hello_world".ToCamelCase();                   // "helloWorld"
"helloWorld".ToSnakeCase();                    // "hello_world"
"the lord of the rings".ToTitleCase(useEnglishMinorWordRules: true);
                                                // "The Lord of the Rings"
"Café de la Gare! 2024".Slugify();             // "cafe-de-la-gare-2024"
```

## Performance

Every method that returns a `string` avoids unnecessary intermediate allocations. As an example, here is the impact of the zero-allocation rewrite of a few methods (measured with [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) on identical hardware, before and after):

| Method | Before | After | Allocated (before → after) |
| --- | --- | --- | --- |
| `IsPalindrome` | 466 ns | 1.6 ns | 648 B → 0 B |
| `CountSubstring` | 59 ns | 1.6 ns | 40 B → 0 B |
| `CountLetters` | 101 ns | 3.7 ns | 32 B → 0 B |

Run the benchmarks yourself:

```bash
dotnet run -c Release --project Benchmarks/Benchmarks.csproj
```

## Tests

- Unit tests: [`UnitTests/StringExtensionTests.cs`](https://github.com/Antyss77/StringExtension/blob/master/UnitTests/StringExtensionTests.cs)
- Performance tests: [`Benchmarks/Benchmark.cs`](https://github.com/Antyss77/StringExtension/blob/master/Benchmarks/Benchmark.cs)

```bash
dotnet test
```

## Contributing

Contributions are welcome, whether that's proposing new methods, reporting bugs, or improving existing code. Open a Pull Request and it will be reviewed.

## License

Distributed under the MIT License. See [LICENSE](https://github.com/Antyss77/StringExtension/blob/master/LICENSE) for details.