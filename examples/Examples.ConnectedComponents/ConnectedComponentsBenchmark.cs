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
    private ConnectedComponentsFinder _connectedComponentsFinder = null!;
    
    private DepthFirstSearchRecursive<int> _dfsRecursive = null!;
    private DepthFirstSearchIterative<int> _dfsIterative = null!;
    private BreadthFirstSearchIterative<int> _bfsIterative = null!;
    private BreadthFirstSearchRecursive<int> _bfsRecursive = null!;
    
    
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

        _connectedComponentsFinder = new ConnectedComponentsFinder();

        _dfsRecursive = new DepthFirstSearchRecursive<int>();
        _dfsIterative = new DepthFirstSearchIterative<int>();
        _bfsRecursive = new BreadthFirstSearchRecursive<int>();
        _bfsIterative = new BreadthFirstSearchIterative<int>();
    }
    
    [Benchmark]
    public async Task FindComponentsWithRecursiveDFS()
        => await _connectedComponentsFinder.FindConnectedComponents(_graph, _dfsRecursive);

    [Benchmark]
    public async Task FindComponentsWithIterativeDFS()
        => await _connectedComponentsFinder.FindConnectedComponents(_graph, _dfsIterative);
    
    [Benchmark]
    public async Task FindComponentsWithRecursiveBFS()
        => await _connectedComponentsFinder.FindConnectedComponents(_graph, _bfsRecursive);

    [Benchmark]
    public async Task FindComponentsWithIterativeBFS()
        => await _connectedComponentsFinder.FindConnectedComponents(_graph, _bfsIterative);
}