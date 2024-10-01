using System.Diagnostics.CodeAnalysis;
using GraphAlgorithms.Analysis.ShortestPaths;
using GraphAlgorithms.Helpers;
using GraphAlgorithms.Model;
using GraphAlgorithms.Traversals;
using GraphAlgorithms.Traversals.Dfs;

namespace GraphAlgorithms;

[ExcludeFromCodeCoverage]
internal static class Program
{
    public static async Task Main()
    {
        await Task.CompletedTask;

        var graph = new Graph<int>(isDirected: true);

        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddVertex(3);
        graph.AddVertex(4);

        graph.AddEdge(1, 2, 4.5);
        graph.AddEdge(1, 3, 2.0);
        graph.AddEdge(3, 4, 1.5);
        graph.AddEdge(2, 4, 3.0);

        graph.PrintGraph();

        var dfsStrategy = new DepthFirstSearchIterative<int>();

        var traversalRunner = new GraphTraversalRunner<int>(dfsStrategy);
        traversalRunner.ExecuteTraversal(graph, 1, async v =>
        {
            Console.WriteLine(v);
            await Task.CompletedTask;
        });
        
        // Dijkstra Example
        var dijkstra = new DijkstraPathFinder<int>(useEarlyExit: true);
        var path1 = dijkstra.GetShortestPath(graph, 1, 4);

        Console.WriteLine(string.Join(" --> ", path1));

        // Bellman-Ford Example
        var bellmanFord = new BellmanFord<int>();
        var path2 = bellmanFord.GetShortestPath(graph, 1, 4);

        Console.WriteLine(string.Join(" --> ", path2));
    }
}