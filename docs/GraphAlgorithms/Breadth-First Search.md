# Breadth-First Search (BFS) in C#

This document provides an overview of the **Breadth-First Search (BFS)** algorithm in C#, with both recursive and
iterative implementations. BFS is an algorithm for traversing or searching tree or graph data structures. It starts at a
chosen root node (or an arbitrary node in the case of a graph) and explores all of the neighbor nodes at the present
depth level before moving on to nodes at the next depth level.

## Table of Contents

- [Introduction to BFS](#introduction-to-bfs)
- [Iterative BFS](#iterative-bfs)
- [Usage Example for Iterative BFS](#usage-example-for-iterative-bfs)
- [Recursive BFS](#recursive-bfs)
- [Usage Example for Recursive BFS](#usage-example-for-recursive-bfs)
- [Comparing Iterative and Recursive BFS](#comparing-iterative-and-recursive-bfs)
- [Conclusion](#conclusion)

---

## Introduction to BFS

BFS is commonly implemented in two main ways:

- **Iterative BFS**: Uses a queue to explore nodes level by level, starting from the root and moving outward.
- **Recursive BFS**: Simulates the queue using recursion, although it's not as commonly used in practice as the
  iterative version.

Both approaches visit all vertices in the graph and are useful for solving problems such as:

- Finding connected components
- Shortest path in unweighted graphs
- Level-order traversal of trees
- Solving puzzles like mazes

---

## Iterative BFS

### Overview

In the iterative implementation of BFS, we use a **queue** to explore the graph level by level. This is the most common
and efficient way to implement BFS, as it avoids the limitations of recursion and can handle deeper graphs without risk
of stack overflow.

### Key Points:

- Iterative BFS uses a queue to explore nodes level by level.
- It ensures that each level of the graph is explored before moving on to the next one.
- Suitable for graphs of any depth or size without risk of stack overflow.

### Code Example:

```csharp
internal class BreadthFirstSearchIterative<T> : GraphTraversalBase<T>, IGraphTraversal<T> where T : notnull
{
    public async Task Traverse(Graph<T> graph, T vertex, Func<T, Task>? onVisit = null)
    {
        Visited.Clear();

        var queue = new Queue<T>();
        queue.Enqueue(vertex);
        Visited.Add(vertex);

        while (queue.Count > 0)
        {
            var currentVertex = queue.Dequeue();

            if (onVisit != null)
                await onVisit(currentVertex);

            foreach (var edge in graph.GetNeighbors(currentVertex))
            {
                if (!Visited.Add(edge.Destination))
                    continue;

                queue.Enqueue(edge.Destination);
            }
        }
    }
}
```

---

### Usage Example for Iterative BFS

```csharp
using System;
using System.Threading.Tasks;

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

        var bfsIterative = new BreadthFirstSearchIterative<int>();

        await bfsIterative.Traverse(graph, 1, async vertex =>
        {
            Console.WriteLine($"Visited {vertex}");
            return Task.CompletedTask;
        });
    }
}
```

---

## Recursive BFS

### Overview

While BFS is typically implemented iteratively, it can also be simulated recursively. In this version, a helper function
is used to process the nodes level by level in a recursive manner, similar to how you would process a queue in an
iterative version. This approach is more academic and less common in practice, as recursion depth can become a
limitation.

### Key Points:

- Recursive BFS simulates the queue using recursion.
- Less common in practice, as it may hit stack depth limitations in very deep graphs.
- Suitable for smaller graphs or for educational purposes.

### Code Example:

```csharp
internal class BreadthFirstSearchRecursive<T> : GraphTraversalBase<T>, IGraphTraversal<T> where T : notnull
{
    public async Task Traverse(Graph<T> graph, T startVertex, Func<T, Task>? onVisit = null)
    {
        Visited.Clear();
        var queue = new Queue<T>();
        queue.Enqueue(startVertex);
        Visited.Add(startVertex);

        await ProcessQueue(graph, queue, onVisit);
    }

    private async Task ProcessQueue(Graph<T> graph, Queue<T> queue, Func<T, Task>? onVisit)
    {
        if (queue.Count == 0) return;

        var vertex = queue.Dequeue();

        if (onVisit != null)
            await onVisit(vertex);

        foreach (var edge in graph.GetNeighbors(vertex))
        {
            if (Visited.Add(edge.Destination))
                queue.Enqueue(edge.Destination);
        }

        await ProcessQueue(graph, queue, onVisit);
    }
}
```

---

### Usage Example for Recursive BFS

```csharp
using System;
using System.Threading.Tasks;

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

        var bfsRecursive = new BreadthFirstSearchRecursive<int>();

        await bfsRecursive.Traverse(graph, 1, async vertex =>
        {
            Console.WriteLine($"Visited {vertex}");
            return Task.CompletedTask;
        });
    }
}
```

---

## Comparing Iterative and Recursive BFS

| Feature                    | Iterative BFS                        | Recursive BFS                                       |
|----------------------------|--------------------------------------|-----------------------------------------------------|
| **Implementation**         | Uses a queue to explore nodes        | Simulates a queue with recursion                    |
| **Stack Overflow Risk**    | No risk, works for large/deep graphs | Potential stack overflow in deep graphs             |
| **Memory Usage**           | Uses an explicit queue               | Uses the call stack for recursion                   |
| **Ease of Implementation** | Standard and commonly used           | Less common and more for academic purposes          |
| **When to Use**            | Use for large or deep graphs         | Suitable for smaller graphs or educational purposes |

---

## Conclusion

Both **iterative** and **recursive BFS** implementations offer powerful tools for graph traversal. The **iterative BFS**
is the preferred choice due to its ability to handle large and deep graphs without the risk of stack overflow. *
*Recursive BFS**, while less common, is useful for educational purposes and works well for small graphs.

In most practical scenarios, **iterative BFS** is more memory-efficient and is the standard approach for BFS traversal.
However, understanding the recursive version provides valuable insights into how BFS can be structured in different
ways.
