```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5011/22H2/2022Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.403
  [Host]     : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2


```
| Method    | N   | Mean     | Error   | StdDev  |
|---------- |---- |---------:|--------:|--------:|
| **Benchmark** | **10**  | **361.2 ns** | **1.81 ns** | **1.60 ns** |
| **Benchmark** | **100** | **289.3 ns** | **5.78 ns** | **5.13 ns** |
| **Benchmark** | **200** | **333.8 ns** | **2.32 ns** | **2.17 ns** |
| **Benchmark** | **400** | **364.8 ns** | **3.23 ns** | **3.02 ns** |
