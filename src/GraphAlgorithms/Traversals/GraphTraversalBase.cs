namespace GraphAlgorithms.Traversals;

internal abstract class GraphTraversalBase<T> where T : notnull
{
    protected readonly HashSet<T> Visited = [];
}