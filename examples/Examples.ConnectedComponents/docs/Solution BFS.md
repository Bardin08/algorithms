# Connected Components Problem Solution

## Problem Definition

In an **undirected graph**, a **connected component** is a maximal set of vertices such that any two vertices are
connected by a path, and no vertex in the set is connected to any vertex outside of it.

The goal is to identify all such connected components in a given graph.

## Generic Solution Using Different Traversal Strategies

We solve this problem using a **generic approach** that accepts different traversal strategies. The solution is flexible
and allows us to provide either **recursive BFS** or **iterative BFS** as the traversal strategy. This is achieved using
a `ConnectedComponentsFinder<T>` class that accepts any implementation of the `IGraphTraversal<T>` interface.

### ConnectedComponentsFinder<T>

The `ConnectedComponentsFinder<T>` class is a generic class that works with any graph traversal strategy. It accepts a
graph and a traversal strategy (e.g., recursive BFS, iterative BFS) to find the connected components.

```csharp
internal class ConnectedComponentsFinder<T>(Graph<T> graph, IGraphTraversal<T> traversalStrategy)
    where T : notnull
{
    private readonly IGraphTraversal<T> _traversalStrategy = traversalStrategy;
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

            // Use the provided traversal strategy to explore the component.
            await _traversalStrategy.Traverse(_graph, vertex, v =>
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
```

The solution allows switching between **recursive BFS** and **iterative BFS** by simply passing the appropriate
strategy.

## Algorithmic Approaches

We use two implementations of **BFS traversal** as strategies in this solution:

1. **Recursive BFS**: Simulates the BFS queue using recursion, although it is less common in practice.
2. **Iterative BFS**: Uses an explicit queue to explore the graph level by level, avoiding the risk of stack overflow.

### Traversal Strategies:

- **[BreadthFirstSearchRecursive.cs](../../../src/GraphAlgorithms/Traversals/Bfs/BreadthFirstSearchRecursive.cs)**: The
  implementation of recursive BFS.
- **[BreadthFirstSearchIterative.cs](../../../src/GraphAlgorithms/Traversals/Bfs/BreadthFirstSearchIterative.cs)**: The
  implementation of iterative BFS.

## Benchmarks

We measured the performance of the **Recursive BFS** and **Iterative BFS** implementations using `BenchmarkDotNet`. The
benchmarks were run on graphs with 1000 and 10,000 vertices, with a 10% edge creation probability. Below are the
results.

### Graph with 1000 vertices

- **Number of vertices**: 1000
- **Edge creation probability**: 0.1
- **Is directed**: false

| Method        |     Mean |  StdDev | Allocated Memory |
|---------------|---------:|--------:|-----------------:|
| Recursive BFS | 651.1 μs | 1.53 μs |        208.15 KB |
| Iterative BFS | 616.2 μs | 1.06 μs |        208.15 KB |

#### Detailed Results for Recursive BFS

```
Mean = 651.113 μs, StdErr = 0.394 μs, StdDev = 1.527 μs
Memory Allocated: 208.15 KB
```

#### Detailed Results for Iterative BFS

```
Mean = 616.187 μs, StdErr = 0.284 μs, StdDev = 1.062 μs
Memory Allocated: 208.15 KB
```

### Graph with 10,000 vertices

- **Number of vertices**: 10,000
- **Edge creation probability**: 0.1
- **Is directed**: false

| Method        |     Mean |   StdDev | Allocated Memory |
|---------------|---------:|---------:|-----------------:|
| Recursive BFS | 199.8 ms | 34.29 ms |          1.96 MB |
| Iterative BFS | 264.3 ms | 33.40 ms |          1.96 MB |

#### Detailed Results for Recursive BFS

```
Mean = 199.847 ms, StdErr = 3.429 ms, StdDev = 34.289 ms
Memory Allocated: 1.96 MB
```

#### Detailed Results for Iterative BFS

```
Mean = 264.305 ms, StdErr = 3.340 ms, StdDev = 33.397 ms
Memory Allocated: 1.96 MB
```

## Conclusion

The **ConnectedComponentsFinder<T>** class provides a flexible solution to find connected components by accepting
different traversal strategies (e.g., recursive or iterative BFS). The benchmarks show that **Iterative BFS** is
generally faster for smaller graphs, while the **Recursive BFS** can have more variability in performance due to
recursion depth.

The choice between recursive and iterative BFS depends on the graph size and depth. For smaller graphs, both approaches
perform similarly, but for larger graphs, **Iterative BFS** tends to handle deep graphs more consistently. Recursive
BFS, while less common in practice, provides an alternative approach for educational purposes or small graphs.