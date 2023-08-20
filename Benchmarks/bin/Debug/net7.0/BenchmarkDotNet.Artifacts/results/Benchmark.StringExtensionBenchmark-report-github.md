```

BenchmarkDotNet v0.13.7, Windows 10 (10.0.19045.3324/22H2/2022Update)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 7.0.306
  [Host]     : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2


```
|                    Method |      Mean |     Error |    StdDev | Allocated |
|-------------------------- |----------:|----------:|----------:|----------:|
|          RemoveCharacters | 368.67 ns |  7.008 ns | 17.058 ns |     328 B |
|              IsValidEmail |  96.72 ns |  2.697 ns |  7.868 ns |         - |
|        IsValidPhoneNumber | 191.88 ns |  3.883 ns | 10.758 ns |         - |
|            CountSubstring |  63.27 ns |  1.306 ns |  1.603 ns |      40 B |
|              ReverseWords |  46.03 ns |  0.977 ns |  1.432 ns |     152 B |
|              IsPalindrome | 648.51 ns | 12.651 ns | 19.319 ns |     648 B |
|              CountLetters | 123.07 ns |  2.478 ns |  6.396 ns |      32 B |
| RemoveDuplicateCharacters | 326.46 ns |  6.632 ns | 19.555 ns |     624 B |
|        ConvertToCamelCase |  65.63 ns |  1.386 ns |  1.753 ns |      48 B |
