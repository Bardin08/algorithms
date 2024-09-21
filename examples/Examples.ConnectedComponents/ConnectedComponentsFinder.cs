using GraphAlgorithms.Model;
using GraphAlgorithms.Traversals;

namespace Examples.ConnectedComponents;

internal class ConnectedComponentsFinder<T>(Graph<T> graph, IGraphTraversal<T> dfsTraversal)
    where T : notnull
{
    private readonly IGraphTraversal<T> _dfsTraversal = dfsTraversal;
    private readonly Graph<T> _graph = graph;

    public async Task<List<HashSet<T>>> FindConnectedComponents()
    {
        var components = new List<HashSet<T>>();
        var visited = new HashSet<T>();

        foreach (var vertex in _graph.AdjacencyList.Keys)
        {
            if (visited.Contains(vertex))
                continue;

            var component = new HashSet<T>();

            // Use the provided DFS traversal strategy (recursive or iterative) to explore the component.
            // Each vertex visited by DFS will be added to the 'visited' set and the current 'component' set.
            await _dfsTraversal.Traverse(_graph, vertex, v =>
            {
                visited.Add(v);
                component.Add(v);
                return Task.CompletedTask;
            });

            components.Add(component);
        }

        return components;
    }
}