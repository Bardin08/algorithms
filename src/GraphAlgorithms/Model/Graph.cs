namespace GraphAlgorithms.Model;

public class Graph<T>(bool isDirected = false) where T : notnull
{
    private readonly Dictionary<T, List<Edge<T>>> _adjacencyList = new();

    public IReadOnlyDictionary<T, IReadOnlyList<Edge<T>>> AdjacencyList =>
        _adjacencyList.ToDictionary(
            pair => pair.Key,
            IReadOnlyList<Edge<T>> (pair) => pair.Value.AsReadOnly()
        );

    public bool IsDirected { get; private set; } = isDirected;

    public void AddVertex(T vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            _adjacencyList[vertex] = [];
        }
    }

    public void AddEdge(T source, T destination, double? weight = null)
    {
        AddVertex(source);
        AddVertex(destination);

        _adjacencyList[source].Add(new Edge<T>(source, destination, weight));

        // If the graph is undirected, add an edge in the opposite direction as well
        if (!IsDirected)
        {
            _adjacencyList[destination].Add(new Edge<T>(destination, source, weight));
        }
    }

    public void RemoveVertex(T vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
            return;

        _adjacencyList.Remove(vertex);

        // Remove any edges pointing to this vertex in other vertices' adjacency lists
        foreach (var edges in _adjacencyList.Values)
        {
            edges.RemoveAll(edge => edge.Destination.Equals(vertex));
        }
    }

    public void RemoveEdge(T source, T destination)
    {
        if (_adjacencyList.TryGetValue(source, out var edge))
            edge.RemoveAll(e => e.Destination.Equals(destination));

        // If the graph is undirected, remove the reverse edge as well
        if (!IsDirected && _adjacencyList.TryGetValue(destination, out edge))
            edge.RemoveAll(e => e.Destination.Equals(source));
    }

    public List<Edge<T>> GetNeighbors(T vertex)
    {
        if (_adjacencyList.TryGetValue(vertex, out var neighbors))
            return neighbors;

        throw new ArgumentException("Vertex does not exist in the graph.");
    }

    public bool ContainsVertex(T vertex) => _adjacencyList.ContainsKey(vertex);
}