using GraphAlgorithms.Model;

namespace Examples.ConnectedComponents;

using BenchmarkDotNet.Attributes;
using GraphAlgorithms;
using GraphAlgorithms.Traversals.Dfs;
using System.Threading.Tasks;

[MemoryDiagnoser, MarkdownExporter]
public class ConnectedComponentsBenchmark
{
    private Graph<int> _graph = null!;
    private ConnectedComponentsFinder<int> componentsFinderRecursive;
    private ConnectedComponentsFinder<int> componentsFinderIterative;

    [GlobalSetup]
    public void Setup()
    {
        var graphGenerator = new GraphGenerator<int>(new GraphGeneratorOptions
        {
            NumberOfVertices = 10000,
            EdgeCreationProbability = 0.1,
            IsDirected = false
        });

        _graph = graphGenerator.Generate(i => i);
        
        var dfsRecursive = new DepthFirstSearchRecursive<int>();
        componentsFinderRecursive = new ConnectedComponentsFinder<int>(_graph, dfsRecursive);

        var dfsIterative = new DepthFirstSearchIterative<int>();
        componentsFinderIterative = new ConnectedComponentsFinder<int>(_graph, dfsIterative);
    }

    [Benchmark]
    public async Task FindComponentsWithRecursiveDFS()
    {
        await componentsFinderRecursive.FindConnectedComponents();
    }

    [Benchmark]
    public async Task FindComponentsWithIterativeDFS()
    {
        await componentsFinderIterative.FindConnectedComponents();
    }
}
