using GraphAlgorithms.Model;

namespace GraphAlgorithms.Traversals;

internal class GraphTraversalRunner<T>(IGraphTraversal<T> traversalStrategy) where T : notnull
{
    private IGraphTraversal<T> _traversalStrategy = traversalStrategy;

    public void SetTraversalStrategy(IGraphTraversal<T> traversalStrategy)
        => _traversalStrategy = traversalStrategy;

    public void ExecuteTraversal(Graph<T> graph, T startVertex, Func<T, Task>? onVisit = null)
    {
        if (_traversalStrategy == null)
            throw new InvalidOperationException("Traversal strategy is not set.");

        _traversalStrategy.Traverse(graph, startVertex, onVisit);
    }
}