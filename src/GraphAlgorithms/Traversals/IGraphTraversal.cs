using GraphAlgorithms.Model;

namespace GraphAlgorithms.Traversals;

internal interface IGraphTraversal<T> where T : notnull
{
    Task Traverse(Graph<T> graph, T startVertex, Func<T, Task>? onVisit = null);
}