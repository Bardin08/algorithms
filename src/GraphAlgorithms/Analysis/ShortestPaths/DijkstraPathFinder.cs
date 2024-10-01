using GraphAlgorithms.Model;

namespace GraphAlgorithms.Analysis.ShortestPaths;

internal class DijkstraPathFinder<T>(bool useEarlyExit) where T : notnull
{
    private bool EarlyExit { get; } = useEarlyExit;

    public IEnumerable<T> GetShortestPath(Graph<T> graph, T start, T end)
    {
        var breadcrumbs = GetShortestPathInternal(graph, start, end);
        return RestorePath(breadcrumbs, start, end);
    }

    private IReadOnlyDictionary<T, T?> GetShortestPathInternal(Graph<T> graph, T start, T end)
    {
        var nextNodes = new PriorityQueue<T, double>();
        var visitedNodes = new Dictionary<T, T?>();
        var weights = new Dictionary<T, double>();

        visitedNodes[start] = default;
        weights[start] = double.NegativeZero;
        nextNodes.Enqueue(start, 0);

        while (nextNodes.Count > 0)
        {
            var current = nextNodes.Dequeue();
            var neighbors = graph.GetNeighbors(current);

            if (current.Equals(end) && EarlyExit)
                break;

            foreach (var neighbor in neighbors.Where(x => !visitedNodes.ContainsKey(x.Destination)))
            {
                var newWeight = neighbor.Weight!.Value;
                if (weights.TryGetValue(neighbor.Destination, out var weight) && newWeight >= weight) 
                    continue;

                weights[neighbor.Destination] = newWeight;
                nextNodes.Enqueue(neighbor.Destination, newWeight);
                visitedNodes[neighbor.Destination] = current;
            }
        }

        return visitedNodes;
    }

    private List<T> RestorePath(
        IReadOnlyDictionary<T, T?> dictionary, T start, T dest)
    {
        var restoredPath = new List<T>();

        var current = dest;
        while (!current.Equals(start))
        {
            restoredPath.Add(current);
            current = dictionary[current]!;
        }

        restoredPath.Add(start);
        restoredPath.Reverse();
        return restoredPath;
    }
}