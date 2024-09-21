# Connected Components Problem Solution

## Problem Definition

In an **undirected graph**, a **connected component** is a maximal set of vertices such that any two vertices are
connected by a path, and no vertex in the set is connected to any vertex outside of it.

The goal is to identify all such connected components in a given graph.

## Generic Solution Using Different Traversal Strategies

We solve this problem using a **generic approach** that accepts different traversal strategies. The solution is flexible
and allows us to provide either **recursive DFS** or **iterative DFS** as the traversal strategy. This is achieved using
a `ConnectedComponentsFinder<T>` class that accepts any implementation of the `IGraphTraversal<T>` interface.

### ConnectedComponentsFinder<T>

The `ConnectedComponentsFinder<T>` class is a generic class that works with any graph traversal strategy. It accepts a
graph and a traversal strategy (e.g., recursive DFS, iterative DFS) to find the connected components.

```csharp
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

            // Use the provided traversal strategy to explore the component.
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
```

The solution allows switching between **recursive DFS** and **iterative DFS** by simply passing the appropriate
strategy.

## Algorithmic Approaches

We use two implementations of **DFS traversal** as strategies in this solution:

1. **Recursive DFS**: A natural fit for graph traversal, using recursion to explore the depth of the graph.
2. **Iterative DFS**: Uses an explicit stack to simulate the recursion, avoiding potential stack overflow for larger
   graphs.

### Traversal Strategies:

- **[DepthFirstSearchRecursive.cs](../../../src/GraphAlgorithms/Traversals/Dfs/DepthFirstSearchRecursive.cs)**: The
  implementation of recursive DFS.
- **[DepthFirstSearchIterative.cs](../../../src/GraphAlgorithms/Traversals/Dfs/DepthFirstSearchIterative.cs)**: The
  implementation of iterative DFS.

## Benchmarks

We measured the performance of the **Recursive DFS** and **Iterative DFS** implementations using `BenchmarkDotNet`. The
benchmarks were run on graphs with 1000 and 10,000 vertices, with a 10% edge creation probability. Below are the
results.

### Graph with 1000 vertices

- **Number of vertices**: 1000
- **Edge creation probability**: 0.1
- **Is directed**: false

| Method        |       Mean |  StdDev | Allocated Memory |
|---------------|-----------:|--------:|-----------------:|
| Recursive DFS |   800.6 μs | 6.73 μs |         168.7 KB |
| Iterative DFS | 1,085.5 μs | 3.84 μs |        681.17 KB |

#### Detailed Results for Recursive DFS

```
Mean = 800.632 μs, StdErr = 1.737 μs, StdDev = 6.727 μs
Memory Allocated: 168.7 KB
```

#### Detailed Results for Iterative DFS

```
Mean = 1.086 ms, StdErr = 0.001 ms, StdDev = 0.004 ms
Memory Allocated: 681.17 KB
```

### Graph with 10,000 vertices

- **Number of vertices**: 10,000
- **Edge creation probability**: 0.1
- **Is directed**: false

| Method        |     Mean |   StdDev | Allocated Memory |
|---------------|---------:|---------:|-----------------:|
| Recursive DFS | 139.3 ms |  4.21 ms |          1.53 MB |
| Iterative DFS | 288.2 ms | 20.10 ms |         65.53 MB |

#### Detailed Results for Recursive DFS

```
Mean = 139.261 ms, StdErr = 0.744 ms, StdDev = 4.209 ms
Memory Allocated: 1.53 MB
```

#### Detailed Results for Iterative DFS

```
Mean = 288.221 ms, StdErr = 2.010 ms, StdDev = 20.103 ms
Memory Allocated: 65.53 MB
```

## Conclusion

The **ConnectedComponentsFinder<T>** class provides a flexible solution to find connected components by accepting
different traversal strategies (e.g., recursive or iterative DFS). The benchmarks show that **Recursive DFS** is
generally faster and more memory-efficient but could suffer from stack overflow on very deep graphs, while **Iterative
DFS** avoids this problem at the cost of increased memory usage and slower performance.

The choice between recursive and iterative DFS depends on the graph size and depth. For smaller graphs, recursive DFS is
more efficient, while iterative DFS is a safer choice for deeper or larger graphs.