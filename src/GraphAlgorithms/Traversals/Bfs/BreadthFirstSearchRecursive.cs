using GraphAlgorithms.Model;

namespace GraphAlgorithms.Traversals.Bfs;

internal class BreadthFirstSearchRecursive<T> : GraphTraversalBase<T>, IGraphTraversal<T> where T : notnull
{
    public async Task Traverse(Graph<T> graph, T startVertex, Func<T, Task>? onVisit = null)
    {
        Visited.Clear();
        var queue = new Queue<T>();
        queue.Enqueue(startVertex);
        Visited.Add(startVertex);

        await ProcessQueue(graph, queue, onVisit);
    }

    private async Task ProcessQueue(Graph<T> graph, Queue<T> queue, Func<T, Task>? onVisit)
    {
        if (queue.Count == 0) return;

        var vertex = queue.Dequeue();

        if (onVisit != null)
            await onVisit(vertex);

        foreach (var edge in graph.GetNeighbors(vertex))
        {
            if (Visited.Add(edge.Destination))
                queue.Enqueue(edge.Destination);
        }

        await ProcessQueue(graph, queue, onVisit);
    }
}