using GraphAlgorithms.Model;

namespace GraphAlgorithms.Traversals.Dfs;

internal class DepthFirstSearchIterative<T>
    : DepthFirstSearchBase<T>, IGraphTraversal<T> where T : notnull
{
    public async Task Traverse(Graph<T> graph, T startVertex, Func<T, Task>? onVisit = null)
    {
        Visited.Clear();

        var stack = new Stack<T>();
        stack.Push(startVertex);

        while (stack.Count > 0)
        {
            var currentVertex = stack.Pop();

            // Returns false if the vertex already in a set of visited vertexes
            if (!Visited.Add(currentVertex))
                continue;

            if (onVisit != null)
                await onVisit(currentVertex);

            // Get neighbors and reverse them before pushing onto the stack
            var neighbors = graph.GetNeighbors(currentVertex);

            neighbors.Reverse();

            foreach (var edge in neighbors)
            {
                if (!Visited.Contains(edge.Destination))
                    stack.Push(edge.Destination);
            }
        }
    }
}