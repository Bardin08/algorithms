using GraphAlgorithms.Model;
using GraphAlgorithms.Traversals.Bfs;

namespace Examples.ConnectedComponents;

using BenchmarkDotNet.Attributes;
using GraphAlgorithms;
using GraphAlgorithms.Traversals.Dfs;
using System.Threading.Tasks;

[MemoryDiagnoser, MarkdownExporter]
public class ConnectedComponentsBenchmark
{
    private Graph<int> _graph = null!;
    private ConnectedComponentsFinder<int> _dfsRecursive = null!;
    private ConnectedComponentsFinder<int> _dfsIterative = null!;
    private ConnectedComponentsFinder<int> _bfsRecursive = null!;
    private ConnectedComponentsFinder<int> _bfsIterative = null!;

    [GlobalSetup]
    public void Setup()
    {
        var graphGenerator = new GraphGenerator<int>(new GraphGeneratorOptions
        {
            NumberOfVertices = 1000,
            EdgeCreationProbability = 0.1,
            IsDirected = false
        });

        _graph = graphGenerator.Generate(i => i);

        var dfsRecursive = new DepthFirstSearchRecursive<int>();
        _dfsRecursive = new ConnectedComponentsFinder<int>(_graph, dfsRecursive);

        var dfsIterative = new DepthFirstSearchIterative<int>();
        _dfsIterative = new ConnectedComponentsFinder<int>(_graph, dfsIterative);

        var bfsRecursive = new BreadthFirstSearchRecursive<int>();
        _bfsRecursive = new ConnectedComponentsFinder<int>(_graph, bfsRecursive);

        var bfsIterative = new BreadthFirstSearchIterative<int>();
        _bfsIterative = new ConnectedComponentsFinder<int>(_graph, bfsIterative);
    }

    // [Benchmark]
    // public async Task FindComponentsWithRecursiveDFS()
    //     => await _dfsRecursive.FindConnectedComponents();
    //
    // [Benchmark]
    // public async Task FindComponentsWithIterativeDFS()
    //     => await _dfsIterative.FindConnectedComponents();

    [Benchmark]
    public async Task FindComponentsWithRecursiveBFS()
        => await _bfsRecursive.FindConnectedComponents();

    [Benchmark]
    public async Task FindComponentsWithIterativeBFS()
        => await _bfsIterative.FindConnectedComponents();
}