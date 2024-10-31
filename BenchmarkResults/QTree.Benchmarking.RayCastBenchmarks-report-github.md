```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5011/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.403
  [Host]     : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2


```
| Method                  | ItemsInTree | Depth | SplitLimit | Mean         | Error       | StdDev      |
|------------------------ |------------ |------ |----------- |-------------:|------------:|------------:|
| **RayCastPoint**            | **10000**       | **5**     | **5**          |  **35,265.8 ns** |   **683.35 ns** | **1,022.81 ns** |
| DynamicTreeRayCastPoint | 10000       | 5     | 5          | 172,338.9 ns | 3,366.68 ns | 4,134.58 ns |
| **RayCastPoint**            | **10000**       | **5**     | **50**         |  **40,555.5 ns** |   **807.47 ns** | **1,021.19 ns** |
| DynamicTreeRayCastPoint | 10000       | 5     | 50         | 161,928.0 ns | 3,150.83 ns | 3,502.13 ns |
| **RayCastPoint**            | **10000**       | **5**     | **100**        |  **41,600.2 ns** |   **786.92 ns** |   **874.66 ns** |
| DynamicTreeRayCastPoint | 10000       | 5     | 100        |     237.9 ns |     4.81 ns |     6.90 ns |
| **RayCastPoint**            | **10000**       | **10**    | **5**          |  **36,947.5 ns** |   **719.90 ns** | **1,009.20 ns** |
| DynamicTreeRayCastPoint | 10000       | 10    | 5          | 209,144.6 ns | 4,026.88 ns | 4,475.86 ns |
| **RayCastPoint**            | **10000**       | **10**    | **50**         |  **40,364.2 ns** |   **802.20 ns** | **1,014.53 ns** |
| DynamicTreeRayCastPoint | 10000       | 10    | 50         | 157,122.2 ns | 3,139.09 ns | 3,855.08 ns |
| **RayCastPoint**            | **10000**       | **10**    | **100**        |  **41,275.1 ns** |   **821.07 ns** | **1,008.34 ns** |
| DynamicTreeRayCastPoint | 10000       | 10    | 100        |     239.5 ns |     4.67 ns |     6.69 ns |
| **RayCastPoint**            | **10000**       | **100**   | **5**          |  **91,642.0 ns** | **1,781.23 ns** | **2,926.61 ns** |
| DynamicTreeRayCastPoint | 10000       | 100   | 5          | 206,320.2 ns |   652.77 ns |   578.66 ns |
| **RayCastPoint**            | **10000**       | **100**   | **50**         |  **39,155.4 ns** |   **225.38 ns** |   **210.82 ns** |
| DynamicTreeRayCastPoint | 10000       | 100   | 50         | 151,230.9 ns |   949.32 ns |   792.73 ns |
| **RayCastPoint**            | **10000**       | **100**   | **100**        |  **41,906.1 ns** |   **831.93 ns** | **1,110.61 ns** |
| DynamicTreeRayCastPoint | 10000       | 100   | 100        |     242.2 ns |     4.91 ns |     7.65 ns |
