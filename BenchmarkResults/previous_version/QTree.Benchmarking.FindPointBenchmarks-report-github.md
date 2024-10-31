```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5011/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.403
  [Host]     : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2


```
| Method               | ItemsInTree | Depth | SplitLimit | Mean        | Error     | StdDev    |
|--------------------- |------------ |------ |----------- |------------:|----------:|----------:|
| **FindPoint**            | **10000**       | **5**     | **5**          |    **631.9 ns** |  **12.46 ns** |  **21.81 ns** |
| DynamicTreeFindPoint | 10000       | 5     | 5          | 50,217.0 ns | 357.53 ns | 316.94 ns |
| **FindPoint**            | **10000**       | **5**     | **50**         |    **678.1 ns** |  **13.40 ns** |  **15.43 ns** |
| DynamicTreeFindPoint | 10000       | 5     | 50         |  8,087.3 ns |  37.03 ns |  34.64 ns |
| **FindPoint**            | **10000**       | **5**     | **100**        |    **682.1 ns** |  **13.69 ns** |  **15.77 ns** |
| DynamicTreeFindPoint | 10000       | 5     | 100        |  6,819.6 ns |   7.96 ns |   7.44 ns |
| **FindPoint**            | **10000**       | **10**    | **5**          |    **808.3 ns** |   **7.42 ns** |   **6.58 ns** |
| DynamicTreeFindPoint | 10000       | 10    | 5          | 57,833.7 ns | 182.14 ns | 152.09 ns |
| **FindPoint**            | **10000**       | **10**    | **50**         |    **675.5 ns** |  **13.00 ns** |  **13.91 ns** |
| DynamicTreeFindPoint | 10000       | 10    | 50         |  7,848.2 ns |  11.70 ns |  10.95 ns |
| **FindPoint**            | **10000**       | **10**    | **100**        |    **698.6 ns** |   **9.12 ns** |   **8.53 ns** |
| DynamicTreeFindPoint | 10000       | 10    | 100        |  6,534.7 ns |  11.08 ns |   8.65 ns |
| **FindPoint**            | **10000**       | **100**   | **5**          |    **856.4 ns** |  **16.13 ns** |  **15.84 ns** |
| DynamicTreeFindPoint | 10000       | 100   | 5          | 41,468.0 ns | 174.92 ns | 155.06 ns |
| **FindPoint**            | **10000**       | **100**   | **50**         |    **697.4 ns** |   **9.61 ns** |   **8.52 ns** |
| DynamicTreeFindPoint | 10000       | 100   | 50         |  8,142.2 ns |  11.74 ns |  10.41 ns |
| **FindPoint**            | **10000**       | **100**   | **100**        |    **707.6 ns** |  **13.53 ns** |  **14.47 ns** |
| DynamicTreeFindPoint | 10000       | 100   | 100        |  6,532.9 ns |  19.00 ns |  15.87 ns |
