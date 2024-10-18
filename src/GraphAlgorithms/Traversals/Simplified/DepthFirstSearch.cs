using GraphAlgorithms.Model;

namespace GraphAlgorithms.Traversals.Simplified;

internal class DepthFirstSearch<T> where T : notnull
{
    private readonly HashSet<T> _visited = [];

    public void Traverse(Graph<T> graph, T startVertex)
    {
        _visited.Clear();
        DFS(graph, startVertex);
    }

    private void DFS(Graph<T> graph, T vertex)
    {
        _visited.Add(vertex);

        Console.WriteLine("Visited: " + vertex);
        foreach (var edge in graph.GetNeighbors(vertex))
        {
            if (!_visited.Contains(edge.Destination))
                DFS(graph, edge.Destination);
        }
    }
}