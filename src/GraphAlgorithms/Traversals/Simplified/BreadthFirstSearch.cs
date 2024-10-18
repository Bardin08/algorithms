using GraphAlgorithms.Model;

namespace GraphAlgorithms.Traversals.Simplified;

internal class BreadthFirstSearch<T> where T : notnull
{
    private readonly HashSet<T> _visited = [];

    public void Traverse(Graph<T> graph, T startVertex)
    {
        _visited.Clear();

        var queue = new Queue<T>();
        queue.Enqueue(startVertex);
        _visited.Add(startVertex);

        while (queue.Count > 0)
        {
            var currentVertex = queue.Dequeue();

            Console.WriteLine("Visited: " + currentVertex);
            foreach (var edge in graph.GetNeighbors(currentVertex))
            {
                if (!_visited.Add(edge.Destination))
                    continue;

                queue.Enqueue(edge.Destination);
            }
        }
    }
}