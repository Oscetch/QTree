```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5011/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.403
  [Host]     : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2


```
| Method              | ItemsInTree | Depth | SplitLimit | Mean      | Error    | StdDev   |
|-------------------- |------------ |------ |----------- |----------:|---------:|---------:|
| **AddPoint**            | **10000**       | **5**     | **5**          |  **82.64 ns** | **0.162 ns** | **0.151 ns** |
| DynamicTreeAddPoint | 10000       | 5     | 5          |  82.71 ns | 0.380 ns | 0.336 ns |
| **AddPoint**            | **10000**       | **5**     | **50**         |  **64.58 ns** | **0.283 ns** | **0.251 ns** |
| DynamicTreeAddPoint | 10000       | 5     | 50         |  81.18 ns | 0.381 ns | 0.356 ns |
| **AddPoint**            | **10000**       | **5**     | **100**        |  **64.62 ns** | **0.293 ns** | **0.245 ns** |
| DynamicTreeAddPoint | 10000       | 5     | 100        |  81.60 ns | 0.529 ns | 0.495 ns |
| **AddPoint**            | **10000**       | **10**    | **5**          | **107.77 ns** | **0.283 ns** | **0.265 ns** |
| DynamicTreeAddPoint | 10000       | 10    | 5          | 135.14 ns | 0.324 ns | 0.271 ns |
| **AddPoint**            | **10000**       | **10**    | **50**         |  **71.14 ns** | **0.192 ns** | **0.170 ns** |
| DynamicTreeAddPoint | 10000       | 10    | 50         | 127.48 ns | 0.234 ns | 0.219 ns |
| **AddPoint**            | **10000**       | **10**    | **100**        |  **65.68 ns** | **0.194 ns** | **0.172 ns** |
| DynamicTreeAddPoint | 10000       | 10    | 100        |  99.86 ns | 0.289 ns | 0.270 ns |
| **AddPoint**            | **10000**       | **100**   | **5**          | **108.04 ns** | **0.323 ns** | **0.302 ns** |
| DynamicTreeAddPoint | 10000       | 100   | 5          | 848.29 ns | 2.068 ns | 1.833 ns |
| **AddPoint**            | **10000**       | **100**   | **50**         |  **65.59 ns** | **0.105 ns** | **0.098 ns** |
| DynamicTreeAddPoint | 10000       | 100   | 50         | 127.20 ns | 0.303 ns | 0.268 ns |
| **AddPoint**            | **10000**       | **100**   | **100**        |  **65.92 ns** | **0.060 ns** | **0.053 ns** |
| DynamicTreeAddPoint | 10000       | 100   | 100        |  97.02 ns | 0.180 ns | 0.160 ns |
