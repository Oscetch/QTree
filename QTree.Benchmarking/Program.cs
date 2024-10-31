
using BenchmarkDotNet.Running;
using QTree.Benchmarking;

var findPointBenchmarks = BenchmarkRunner.Run<FindPointBenchmarks>();
var addBenchmarks = BenchmarkRunner.Run<AddBenchmarks>();
var rayCastBenchMarks = BenchmarkRunner.Run<RayCastBenchmarks>();

var findPointMarkdownFilePath = Path.Combine(findPointBenchmarks.ResultsDirectoryPath, $"QTree.Benchmarking.{nameof(FindPointBenchmarks)}-report-github.md");
var addMarkdownFilePath = Path.Combine(addBenchmarks.ResultsDirectoryPath, $"QTree.Benchmarking.{nameof(AddBenchmarks)}-report-github.md");
var rayCastMarkdownFilePath= Path.Combine(rayCastBenchMarks.ResultsDirectoryPath, $"QTree.Benchmarking.{nameof(RayCastBenchmarks)}-report-github.md");

Console.WriteLine($"{nameof(FindPointBenchmarks)} can be found at {findPointMarkdownFilePath}");
Console.WriteLine($"{nameof(AddBenchmarks)} can be found at {addMarkdownFilePath}");
Console.WriteLine($"{nameof(RayCastBenchmarks)} can be found at {rayCastMarkdownFilePath}");
