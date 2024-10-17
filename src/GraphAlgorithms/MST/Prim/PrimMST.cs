using GraphAlgorithms.Model;

namespace GraphAlgorithms.MST.Prim;

internal class PrimMST<T>(Graph<T> graph) where T : notnull
{
    private readonly Graph<T> _graph = graph;
    private readonly List<Edge<T>> _mstEdges = [];

    public List<Edge<T>> FindMST(T startVertex)
    {
        var visited = new HashSet<T>();
        var minHeap = new MinHeap<Edge<T>>((edge1, edge2) =>
        {
            if (edge1.Weight > edge2.Weight) return 1;
            if (edge1.Weight.Equals(edge2.Weight)) return 0;
            return -1;
        });

        foreach (var edge in _graph.GetNeighbors(startVertex))
        {
            minHeap.Insert(edge);
        }

        visited.Add(startVertex);

        while (minHeap.Count > 0 && visited.Count < _graph.AdjacencyList.Count)
        {
            var minEdge = minHeap.ExtractMin();

            if (!visited.Contains(minEdge.Destination))
            {
                _mstEdges.Add(minEdge);
                visited.Add(minEdge.Destination);

                foreach (var edge in _graph.GetNeighbors(minEdge.Destination))
                {
                    if (!visited.Contains(edge.Destination))
                        minHeap.Insert(edge);
                }
            }

            if (_mstEdges.Count == _graph.AdjacencyList.Count - 1)
                break;
        }

        return _mstEdges;
    }
}