```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5011/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.403
  [Host]     : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2


```
| Method              | ItemsInTree | Depth | SplitLimit | Mean        | Error    | StdDev   |
|-------------------- |------------ |------ |----------- |------------:|---------:|---------:|
| **AddPoint**            | **10000**       | **5**     | **5**          |    **76.11 ns** | **0.387 ns** | **0.343 ns** |
| DynamicTreeAddPoint | 10000       | 5     | 5          |    63.46 ns | 0.129 ns | 0.101 ns |
| **AddPoint**            | **10000**       | **5**     | **50**         |    **75.57 ns** | **0.195 ns** | **0.173 ns** |
| DynamicTreeAddPoint | 10000       | 5     | 50         |    62.44 ns | 0.172 ns | 0.144 ns |
| **AddPoint**            | **10000**       | **5**     | **100**        |    **75.32 ns** | **0.203 ns** | **0.180 ns** |
| DynamicTreeAddPoint | 10000       | 5     | 100        |    63.02 ns | 0.103 ns | 0.086 ns |
| **AddPoint**            | **10000**       | **10**    | **5**          |   **149.02 ns** | **0.098 ns** | **0.077 ns** |
| DynamicTreeAddPoint | 10000       | 10    | 5          |   112.72 ns | 0.027 ns | 0.024 ns |
| **AddPoint**            | **10000**       | **10**    | **50**         |   **136.47 ns** | **0.490 ns** | **0.435 ns** |
| DynamicTreeAddPoint | 10000       | 10    | 50         |   110.62 ns | 0.056 ns | 0.050 ns |
| **AddPoint**            | **10000**       | **10**    | **100**        |   **136.97 ns** | **0.130 ns** | **0.109 ns** |
| DynamicTreeAddPoint | 10000       | 10    | 100        |    86.66 ns | 0.603 ns | 0.503 ns |
| **AddPoint**            | **10000**       | **100**   | **5**          | **1,566.47 ns** | **1.699 ns** | **1.419 ns** |
| DynamicTreeAddPoint | 10000       | 100   | 5          |   763.44 ns | 0.965 ns | 0.754 ns |
| **AddPoint**            | **10000**       | **100**   | **50**         | **1,537.21 ns** | **2.169 ns** | **1.923 ns** |
| DynamicTreeAddPoint | 10000       | 100   | 50         |   107.87 ns | 0.143 ns | 0.127 ns |
| **AddPoint**            | **10000**       | **100**   | **100**        | **1,586.48 ns** | **2.699 ns** | **2.107 ns** |
| DynamicTreeAddPoint | 10000       | 100   | 100        |    80.10 ns | 0.376 ns | 0.334 ns |
