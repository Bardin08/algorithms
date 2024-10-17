using System.Diagnostics.CodeAnalysis;
using GraphAlgorithms.Analysis.ShortestPaths;
using GraphAlgorithms.Helpers;
using GraphAlgorithms.Model;
using GraphAlgorithms.MST.Kruskal;
using GraphAlgorithms.MST.Prim;
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
        graph.AddEdge(2, 4, -3.0);

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

        var kruskalGraph = new Graph<string>(isDirected: false);
        
        kruskalGraph.AddVertex("A");
        kruskalGraph.AddVertex("B");
        kruskalGraph.AddVertex("C");
        kruskalGraph.AddVertex("D");
        kruskalGraph.AddVertex("E");
        
        kruskalGraph.AddEdge("A", "B", 10);
        kruskalGraph.AddEdge("A", "C", 20);
        kruskalGraph.AddEdge("B", "C", 30);
        kruskalGraph.AddEdge("B", "D", 50);
        kruskalGraph.AddEdge("C", "D", 40);
        kruskalGraph.AddEdge("C", "E", 60);
        kruskalGraph.AddEdge("D", "E", 70);
        
        var kruskalMST = new KruskalMST<string>(kruskalGraph);
        var mstEdgesKruskal = kruskalMST.FindMST();
        Console.WriteLine($"Min length of Kruskal MST: {mstEdgesKruskal.Sum(x => x.Weight)}");
        foreach (var edge in mstEdgesKruskal)
        {
            Console.WriteLine($"{edge.Source} -> {edge.Destination} (Weight: {edge.Weight})");
        }
        
        var primMST = new PrimMST<string>(kruskalGraph);
        var mstEdges = primMST.FindMST("A");
        Console.WriteLine("Prim's MST:");
        foreach (var edge in mstEdges)
        {
            Console.WriteLine($"{edge.Source} -> {edge.Destination} (Weight: {edge.Weight})");
        }
    }
}