```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5011/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.403
  [Host]     : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2


```
| Method               | ItemsInTree | Depth | SplitLimit | Mean         | Error      | StdDev     |
|--------------------- |------------ |------ |----------- |-------------:|-----------:|-----------:|
| **FindPoint**            | **10000**       | **5**     | **5**          |    **270.90 ns** |   **5.477 ns** |  **12.137 ns** |
| DynamicTreeFindPoint | 10000       | 5     | 5          | 30,223.47 ns | 223.111 ns | 208.698 ns |
| **FindPoint**            | **10000**       | **5**     | **50**         |    **450.84 ns** |   **8.862 ns** |  **11.831 ns** |
| DynamicTreeFindPoint | 10000       | 5     | 50         |  5,978.30 ns |  35.328 ns |  33.046 ns |
| **FindPoint**            | **10000**       | **5**     | **100**        |    **670.52 ns** |  **12.310 ns** |  **11.515 ns** |
| DynamicTreeFindPoint | 10000       | 5     | 100        |     38.20 ns |   0.815 ns |   1.088 ns |
| **FindPoint**            | **10000**       | **10**    | **5**          |    **305.56 ns** |   **6.058 ns** |   **9.953 ns** |
| DynamicTreeFindPoint | 10000       | 10    | 5          | 34,925.07 ns |  70.888 ns |  66.309 ns |
| **FindPoint**            | **10000**       | **10**    | **50**         |    **451.99 ns** |   **8.863 ns** |  **10.885 ns** |
| DynamicTreeFindPoint | 10000       | 10    | 50         |  5,921.33 ns |  12.147 ns |  10.143 ns |
| **FindPoint**            | **10000**       | **10**    | **100**        |    **660.98 ns** |  **12.755 ns** |  **14.689 ns** |
| DynamicTreeFindPoint | 10000       | 10    | 100        |     37.82 ns |   0.814 ns |   1.030 ns |
| **FindPoint**            | **10000**       | **100**   | **5**          |    **297.16 ns** |   **5.962 ns** |   **9.796 ns** |
| DynamicTreeFindPoint | 10000       | 100   | 5          | 43,150.67 ns | 145.498 ns | 136.099 ns |
| **FindPoint**            | **10000**       | **100**   | **50**         |    **436.63 ns** |   **8.777 ns** |   **8.620 ns** |
| DynamicTreeFindPoint | 10000       | 100   | 50         |  5,922.11 ns |  21.402 ns |  20.020 ns |
| **FindPoint**            | **10000**       | **100**   | **100**        |    **676.08 ns** |  **13.486 ns** |  **14.430 ns** |
| DynamicTreeFindPoint | 10000       | 100   | 100        |     38.97 ns |   0.833 ns |   1.054 ns |
