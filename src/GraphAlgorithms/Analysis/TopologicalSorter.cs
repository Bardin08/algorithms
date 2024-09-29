using GraphAlgorithms.Model;

namespace GraphAlgorithms.Analysis;

internal class TopologicalSorter<T> where T : notnull
{
    private Graph<T> _graph = null!;
    
    private readonly Stack<T> _sorted = new();
    private readonly HashSet<T> _visited = new();

    public List<T> Sort(Graph<T> graph)
    {
        if (!graph.IsDirected)
        {
            throw new ArgumentException("Graph must be directed", nameof(graph));
        }

        _graph = graph;

        foreach (var vertex in graph.AdjacencyList.Keys)
        {
            if (_visited.Add(vertex))
            {
                TraverseAndSort(vertex);
            }
        }

        return _sorted.ToList();
    }

    private void TraverseAndSort(T vertex)
    {
        _visited.Add(vertex);
        
        foreach (var neighbor in _graph.GetNeighbors(vertex))
        {
            if (!_visited.Contains(neighbor.Destination))
            {
                TraverseAndSort(neighbor.Destination);
            }
        }
        
        _sorted.Push(vertex);
    }
}