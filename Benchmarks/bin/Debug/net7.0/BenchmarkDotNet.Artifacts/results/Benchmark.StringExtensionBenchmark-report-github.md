```

BenchmarkDotNet v0.13.7, Windows 10 (10.0.19045.3324/22H2/2022Update)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 7.0.306
  [Host]     : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2


```
|                    Method |      Mean |    Error |    StdDev | Allocated |
|-------------------------- |----------:|---------:|----------:|----------:|
|          RemoveCharacters | 305.79 ns | 5.967 ns |  8.168 ns |     328 B |
|              IsValidEmail |  79.97 ns | 1.640 ns |  4.625 ns |         - |
|        IsValidPhoneNumber | 170.67 ns | 3.396 ns |  5.083 ns |         - |
|            CountSubstring |  64.06 ns | 0.470 ns |  0.417 ns |      40 B |
|              ReverseWords |  44.00 ns | 0.954 ns |  1.949 ns |     152 B |
|              IsPalindrome | 416.24 ns | 8.134 ns | 16.245 ns |     536 B |
|              CountLetters | 104.75 ns | 0.732 ns |  0.612 ns |      32 B |
| RemoveDuplicateCharacters | 253.15 ns | 3.618 ns |  3.021 ns |     624 B |
|        ConvertToCamelCase |  45.49 ns | 0.214 ns |  0.167 ns |      48 B |
