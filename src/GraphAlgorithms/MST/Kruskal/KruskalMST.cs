using GraphAlgorithms.Model;

namespace GraphAlgorithms.MST.Kruskal;

internal class KruskalMST<T>(Graph<T> graph)
    where T : notnull
{
    private readonly Graph<T> _graph = graph;
    private readonly List<Edge<T>> _mstEdges = [];

    public List<Edge<T>> FindMST()
    {
        var disjointSet = new DisjointSet<T>(_graph.AdjacencyList.Keys);

        // Step 1: Get all edges and sort them by weight
        var edgeByWeight = _graph.AdjacencyList
            .SelectMany(v => _graph.GetNeighbors(v.Key))
            .OrderBy(x => x.Weight);

        // Step 2: Add edges to the MST, checking for cycles with Union-Find
        foreach (var edge in edgeByWeight)
        {
            if (!disjointSet.Find(edge.Source).Equals(disjointSet.Find(edge.Destination)))
            {
                disjointSet.Union(edge.Source, edge.Destination);
                _mstEdges.Add(edge);
            }

            // Stop if we've added V-1 edges (i.e., the MST is complete)
            if (_mstEdges.Count == _graph.AdjacencyList.Count - 1)
                break;
        }

        return _mstEdges;
    }
}