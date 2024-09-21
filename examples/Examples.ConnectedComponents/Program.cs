using BenchmarkDotNet.Running;
using Examples.ConnectedComponents;

var summary = BenchmarkRunner.Run<ConnectedComponentsBenchmark>();