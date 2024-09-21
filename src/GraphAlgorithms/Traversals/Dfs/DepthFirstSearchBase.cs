namespace GraphAlgorithms.Traversals.Dfs;

internal abstract class DepthFirstSearchBase<T> where T : notnull
{
    protected readonly HashSet<T> Visited = [];
}