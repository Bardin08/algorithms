using GraphAlgorithms.Model;

namespace GraphAlgorithms.Traversals.Bfs;

internal class BreadthFirstSearchIterative<T> : GraphTraversalBase<T>, IGraphTraversal<T> where T : notnull
{
    public async Task Traverse(Graph<T> graph, T vertex, Func<T, Task>? onVisit = null)
    {
        Visited.Clear();

        var queue = new Queue<T>();
        queue.Enqueue(vertex);
        Visited.Add(vertex);

        while (queue.Count > 0)
        {
            var currentVertex = queue.Dequeue();

            if (onVisit != null)
                await onVisit(currentVertex);

            foreach (var edge in graph.GetNeighbors(currentVertex))
            {
                if (!Visited.Add(edge.Destination))
                    continue;

                queue.Enqueue(edge.Destination);
            }
        }
    }
}