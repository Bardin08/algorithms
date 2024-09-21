# Depth-First Search (DFS) in C#

This document provides an overview of the **Depth-First Search (DFS)** algorithm in C#, with both **recursive** and
**iterative** implementations. DFS is an algorithm for traversing or searching tree or graph data structures. The
algorithm starts at the root node (or an arbitrary node in the case of a graph) and explores as far as possible along
each branch before backtracking.

## Table of Contents

1. [Introduction to DFS](#introduction-to-dfs)
2. [Recursive DFS](#recursive-dfs)
    - [Usage Example for Recursive DFS](#usage-example-for-recursive-dfs)
3. [Iterative DFS](#iterative-dfs)
    - [Usage Example for Iterative DFS](#usage-example-for-iterative-dfs)
4. [Comparing Recursive and Iterative DFS](#comparing-recursive-and-iterative-dfs)
5. [Conclusion](#conclusion)

---

## Introduction to DFS

DFS can be implemented in two main ways:

1. **Recursive DFS**: A natural fit for DFS as it follows the depth-first nature by recursively visiting each branch
   before backtracking.
2. **Iterative DFS**: Achieves the same result as recursive DFS but uses a stack to avoid potential stack overflow
   issues with deep recursion.

Both approaches visit all vertices in the graph and can be used to solve problems like:

- Finding connected components
- Detecting cycles
- Solving puzzles like mazes
- Topological sorting

---

## Recursive DFS

### Overview

In the **recursive implementation**, DFS is implemented using recursion. This is a direct approach where the function
calls itself to traverse deeper into the graph until no more vertices can be visited, then backtracks.

### Key Points:

- Recursive DFS is simple and easy to understand.
- Risk of **stack overflow** in very deep or large graphs.
- Suitable for most common cases with moderate graph sizes.

### Code Example:

```csharp
internal class DepthFirstSearchRecursive<T>
    : DepthFirstSearchBase<T>, IGraphTraversal<T> where T : notnull
{
    public async Task TraverseAsync(Graph<T> graph, T vertex, Func<T, Task> onVisitAsync)
    {
        Visited.Clear();
        await DFS(graph, vertex, onVisitAsync); // Start DFS traversal
    }

    private async Task DFS(Graph<T> graph, T vertex, Func<T, Task> onVisitAsync)
    {
        Visited.Add(vertex);

        if (onVisitAsync != null)
            await onVisitAsync(vertex);

        foreach (var edge in graph.GetNeighbors(vertex))
        {
            if (!Visited.Contains(edge.Destination))
                await DFS(graph, edge.Destination, onVisitAsync);
        }
    }
}
```

### Usage Example for Recursive DFS

```csharp
using System;
using System.Threading.Tasks;
using Algorithms.Graphs;

class Program
{
    static async Task Main()
    {
        var graph = new Graph<int>(isDirected: false);

        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddVertex(3);
        graph.AddVertex(4);

        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 4);

        var dfsRecursive = new DepthFirstSearchRecursive<int>();
        await dfsRecursive.TraverseAsync(graph, 1, async vertex =>
        {
            await Task.CompleteTask;
            Console.WriteLine($"Visited {vertex}");
        });
    }
}
```

---

## Iterative DFS

### Overview

The **iterative DFS** implementation uses a **stack** to simulate the recursive calls. This avoids the risk of stack
overflow and can handle deeper graphs more efficiently in terms of memory usage.

### Key Points:

- Iterative DFS avoids the risk of **stack overflow** in large or deep graphs.
- Requires managing the stack explicitly.
- The **order of visiting vertices** can differ from the recursive DFS unless you **reverse** the order of neighbors
  before pushing them onto the stack.

### Code Example:

```csharp
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
            var vertex = stack.Pop();

            if (!Visited.Contains(vertex))
            {
                Visited.Add(vertex);

                if (onVisit != null)
                    await onVisit(vertex);

                // Get neighbors and reverse them before pushing onto the stack
                var neighbors = graph.GetNeighbors(vertex);

                neighbors.Reverse();

                foreach (var edge in neighbors)
                {
                    if (!Visited.Contains(edge.Destination))
                        stack.Push(edge.Destination);
                }
            }
        }
    }
}
```

### Usage Example for Iterative DFS

```csharp
using System;
using System.Threading.Tasks;
using Algorithms.Graphs;

class Program
{
    static async Task Main()
    {
        var graph = new Graph<int>(isDirected: false);

        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddVertex(3);
        graph.AddVertex(4);

        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 4);

        var dfsIterative = new DepthFirstSearchIterative<int>();

        await dfsIterative.Traverse(graph, 1, async vertex =>
        {
            await Task.CompleteTask;
            Console.WriteLine($"Visited {vertex}");
        });
    }
}
```

---

## Comparing Recursive and Iterative DFS

| Feature                    | Recursive DFS                   | Iterative DFS                         |
|----------------------------|---------------------------------|---------------------------------------|
| **Implementation**         | Uses recursion to visit nodes   | Uses a stack to simulate recursion    |
| **Stack Overflow**         | Risk in large/deep graphs       | No risk of stack overflow             |
| **Memory Usage**           | Uses the call stack             | Uses an explicit stack (manageable)   |
| **Order of Visits**        | Natural order of visiting nodes | Requires reversing neighbors to match |
| **Ease of Implementation** | Simple and direct to implement  | Requires explicit stack management    |

### When to Use:

- Use **recursive DFS** for small to medium graphs where recursion depth isn't an issue.
- Use **iterative DFS** for very large or deep graphs where stack overflow might be a concern.

---

## Conclusion

Both recursive and iterative DFS implementations are powerful tools for graph traversal. The choice between the two
depends on the size and depth of the graph, as well as the environment in which the algorithm is used. The
**iterative DFS** avoids stack overflow and may be preferable in production systems handling large graphs, while the
**recursive DFS** offers a simpler and more intuitive implementation for smaller datasets.
