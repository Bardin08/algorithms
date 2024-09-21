using System.Diagnostics.CodeAnalysis;
using GraphAlgorithms.Helpers;
using GraphAlgorithms.Model;
using GraphAlgorithms.Traversals;
using GraphAlgorithms.Traversals.Dfs;

namespace GraphAlgorithms;

[ExcludeFromCodeCoverage]
internal static partial class Program
{
    public static async Task Main(string[] args)
    {
        await Task.CompletedTask;

        // Create an undirected graph of integers
        var graph = new Graph<int>(isDirected: false);

        // Add vertices and edges
        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddVertex(3);
        graph.AddVertex(4);

        // Add edges with weights
        graph.AddEdge(1, 2, 4.5); // Edge from 1 to 2 with weight 4.5
        graph.AddEdge(1, 3, 2.0); // Edge from 1 to 3 with weight 2.0
        graph.AddEdge(3, 4, 1.5); // Edge from 3 to 4 with weight 1.5
        graph.AddEdge(2, 4, 3.0); // Edge from 2 to 4 with weight 3.0

        // Print the graph
        graph.PrintGraph();
        
        // Create a DFS traversal strategy
        var dfsStrategy = new DepthFirstSearchIterative<int>();

        // Create the traversal runner with DFS strategy
        var traversalRunner = new GraphTraversalRunner<int>(dfsStrategy);
        traversalRunner.ExecuteTraversal(graph, 1, async v =>
        {
            Console.WriteLine(v);
            await Task.CompletedTask;
        });
    }
}