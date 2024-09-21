using GraphAlgorithms.Model;

namespace GraphAlgorithms.Helpers;

public static class GraphHelper
{
    public static void PrintGraph<T>(this Graph<T> graph) where T : notnull
    {
        foreach (var vertex in graph.AdjacencyList)
        {
            Console.Write($"{vertex.Key}: ");
            foreach (var edge in vertex.Value)
            {
                Console.Write($" -> {edge.Destination} (Weight: {edge.Weight ?? 0})");
            }

            Console.WriteLine();
        }
    }
}