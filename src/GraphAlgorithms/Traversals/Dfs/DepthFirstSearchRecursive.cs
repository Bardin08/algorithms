using GraphAlgorithms.Model;

namespace GraphAlgorithms.Traversals.Dfs;

internal class DepthFirstSearchRecursive<T>
    : DepthFirstSearchBase<T>, IGraphTraversal<T> where T : notnull
{
    public async Task Traverse(Graph<T> graph, T vertex, Func<T, Task>? onVisit = null)
    {
        Visited.Clear();
        await DFS(graph, vertex, onVisit);
    }

    private async Task DFS(Graph<T> graph, T vertex, Func<T, Task>? onVisit)
    {
        Visited.Add(vertex);

        if (onVisit != null)
            await onVisit(vertex);

        foreach (var edge in graph.GetNeighbors(vertex))
        {
            if (!Visited.Contains(edge.Destination))
                await DFS(graph, edge.Destination, onVisit);
        }
    }
}