using GraphAlgorithms.Model;

namespace GraphAlgorithms.Analysis.ShortestPaths;

internal class BellmanFord<T> where T : notnull
{

    public IEnumerable<T> GetShortestPath(Graph<T> graph, T start, T destination)
    {
        var breadcrumbs = EvaluateGraph(graph, start);
        if (breadcrumbs is null)
            throw new Exception("Negative cycle path found");

        return RestorePath(breadcrumbs, start, destination);
    }

    private IReadOnlyDictionary<T, T?>? EvaluateGraph(Graph<T> graph, T source)
    {
        var weightedNodes = new Dictionary<T, T?>();
        var weights = new Dictionary<T, double>();

        // Step 1: Initialize distances
        foreach (var vertex in graph.AdjacencyList.Keys)
        {
            weights[vertex] = double.PositiveInfinity;
            weightedNodes[vertex] = default;
        }

        weights[source] = 0;

        for (var i = 0; i < graph.AdjacencyList.Count - 1; i++)
        {
            foreach (var vertex in graph.AdjacencyList.Keys)
            {
                foreach (var edge in graph.GetNeighbors(vertex))
                {
                    var neighbor = edge.Destination;
                    var weight = edge.Weight.GetValueOrDefault(defaultValue: 1);

                    if (!double.IsPositiveInfinity(weights[vertex]) &&
                        weights[vertex] + weight < weights[neighbor])
                    {
                        weights[neighbor] = weights[vertex] + weight;
                        weightedNodes[neighbor] = vertex;
                    }
                }
            }
        }

        foreach (var vertex in graph.AdjacencyList.Keys)
        {
            foreach (var edge in graph.GetNeighbors(vertex))
            {
                var neighbor = edge.Destination;
                var weight = edge.Weight;

                if (!double.IsPositiveInfinity(weights[vertex]) &&
                    weights[vertex] + weight < weights[neighbor])
                {
                    return null; // Negative weight cycle found
                }
            }
        }

        return weightedNodes;
    }

    private List<T> RestorePath(IReadOnlyDictionary<T, T?> breadcrumbs, T start, T dest)
    {
        var restoredPath = new List<T>();

        var current = dest;
        while (!current.Equals(start))
        {
            restoredPath.Add(current);
            current = breadcrumbs[current]!;
        }

        restoredPath.Add(start);
        restoredPath.Reverse();

        return restoredPath;
    }
}